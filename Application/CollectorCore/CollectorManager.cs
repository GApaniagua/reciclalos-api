using AutoMapper;
using Infrastructure.Persistence.Interface;
using Domain.Collector;
using Application.CollectorCore.Dto;
using Infrastructure.Persistence.Models;
using Application.CollectorCore.CreateCollectorStrategy;
using Application.AuthCore;
// using Application.CatalogCore.Dto;

namespace Application.CollectorCore;

public class CollectorManager : ICollectorService
{
  private readonly IAuthService _authService;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public CollectorManager(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService)
  {
    this._unitOfWork = unitOfWork;
    this._mapper = mapper;
    this._authService = authService;
  }

  public async Task CreateCollector(CreateCollector collectorDto)
  {
    try
    {
      var collector = _mapper.Map<Collector>(collectorDto);
      collector.created = DateTime.Now;
      collector.status = "PENDING";

      var strategy = CollectorCreationStrategyFactory.GetStrategy(collector.type);
      await strategy.CreateEntity(
        collector,
        _unitOfWork,
        _mapper
      );


    }
    catch (Exception error)
    {
      throw new InvalidOperationException($"{error.Message}");
    }
  }

  public async Task CreateCollectorWithMaterials(CreateCollectorWithMaterialsDTO collector)
  {
    try
    {
      var collectorId = collector.CollectorId;
      var collectionCenterId = collector.CollectionCenterId;
      var materials = collector.Materials;
      var location = await this._unitOfWork.Repository.LocationRepository.SingleOrDefaultAsync(x => x.Id == collectionCenterId);
      var user = await this._unitOfWork.Repository.UserRepository.SingleOrDefaultAsync(x => x.Id == collectorId);
      if (location == null)
      {
        throw new InvalidOperationException("el id del centro de acopio es inválido");
      }
      if (user == null)
      {
        throw new InvalidOperationException("el id del recolector es inválido");
      }
      if (user.Type != "U")
      {
        throw new InvalidOperationException("el Rol del recolector es inválido");
      }
      var collection = await this._unitOfWork.Repository.CollectionRepository.SingleOrDefaultAsync(x => x.IdUser == (short)collectorId && x.IdLocation == (short)collectionCenterId);
      var isUpdate = true;
      if (collection == null)
      {
        collection = new Collection();
        collection.Created = DateTime.Now;
        collection.IdLocation = (short)collectionCenterId;
        collection.IdUser = (short)collectorId;
        isUpdate = false;
      }

      // to update collection
      foreach (var material in materials)
      {
        switch (material.MaterialType)
        {
          case "Latas":
            collection.Latas += material.Quantity;
            break;
          case "Papel y cartón":
            collection.Papel += material.Quantity;
            break;
          case "Plasticos PET":
            collection.PlasticoPet += material.Quantity;
            break;
          case "Plasticos Otros":
            collection.PlasticoOtros += material.Quantity;
            break;
          case "Envases y botellas de vidrio":
            collection.Vidrio += material.Quantity;
            break;
          case "Multicapa":
            collection.Tetrapak += material.Quantity;
            break;
          default:
            Console.WriteLine("Material no reconocido: " + material.MaterialType);
            break;
        }
      }
      if (isUpdate)
      {
        collection.DateUpdated = DateTime.Now;
        this._unitOfWork.Repository.CollectionRepository.Update(collection);
      }
      else
      {
        await this._unitOfWork.Repository.CollectionRepository.AddAsync(collection);
      }
      await this._unitOfWork.SaveChangesAsync();
    }
    catch (Exception error)
    {
      throw new InvalidOperationException($"{error.Message}");
    }
  }
  public async Task CreateCollectorSellMaterials(CreateCollectorSellMaterialsDTO collector)
  {
    try
    {
      var collectorId = collector.CollectorId;
      var collectionCenterId = collector.CollectionCenterId;
      var materials = collector.Materials;
      var location = await this._unitOfWork.Repository.LocationRepository.SingleOrDefaultAsync(x => x.Id == collectionCenterId);
      var user = await this._unitOfWork.Repository.UserRepository.SingleOrDefaultAsync(x => x.Id == collectorId);
      if (location == null)
      {
        throw new InvalidOperationException("el id del centro de acopio es inválido");
      }
      if (user == null)
      {
        throw new InvalidOperationException("el id del recolector es inválido");
      }
      if (user.Type != "U")
      {
        throw new InvalidOperationException("el Rol del recolector es inválido");
      }
      var collection = await this._unitOfWork.Repository.CollectionRepository.SingleOrDefaultAsync(x => x.IdUser == (short)collectorId && x.IdLocation == (short)collectionCenterId);
      if (collection == null)
      {
        throw new InvalidOperationException("el recolector no puede ejercer acción sobre venta de materiales ya que no se encuentra su registro ");
      }

      // to update collection
      foreach (var material in materials)
      {
        switch (material.MaterialType)
        {
          case "Latas":
            collection.Latas -= material.Quantity;
            break;
          case "Papel y cartón":
            collection.Papel -= material.Quantity;
            break;
          case "Plasticos PET":
            collection.PlasticoPet -= material.Quantity;
            break;
          case "Plasticos Otros":
            collection.PlasticoOtros -= material.Quantity;
            break;
          case "Envases y botellas de vidrio":
            collection.Vidrio -= material.Quantity;
            break;
          case "Multicapa":
            collection.Tetrapak -= material.Quantity;
            break;
          default:
            Console.WriteLine("Material no reconocido: " + material.MaterialType);
            break;
        }
      }
      collection.DateUpdated = DateTime.Now;
      this._unitOfWork.Repository.CollectionRepository.Update(collection);
      await this._unitOfWork.SaveChangesAsync();
    }
    catch (Exception error)
    {
      throw new InvalidOperationException($"{error.Message}");
    }
  }

  public async Task<IEnumerable<CollectorViewDto>> GetAllCollectorsUseCase()
  {
    try
    {
      var users = await this._unitOfWork.Repository.UserRepository.GetAllAsync();
      var collectors = _mapper.Map<IEnumerable<Collector>>(users);
      var collectorsDto = _mapper.Map<IEnumerable<CollectorViewDto>>(collectors);
      var roles = await this._unitOfWork.Repository.RoleRepository.GetAllAsync();
      var collectorTypes = this._mapper.Map<IEnumerable<CollectorTypeDto>>(roles);
      foreach (var collector in collectorsDto ?? Enumerable.Empty<CollectorViewDto>())
      {
        var userType = collectorTypes.FirstOrDefault(m => m.name == collector.collectorTypeId);
        collector.type = userType;
      }

      return collectorsDto;
    }
    catch (Exception error)
    {
      throw new InvalidOperationException($"{error.Message}");
    }
  }

  public async Task<CollectorAdminViewDto> GetAdminAllCollector(string token)
  {
    var claim = this._authService.GetPrincipalFromExpiredToken(token);
    var username = claim.FindFirst("username")?.Value;
    if (username == null)
    {
      throw new Exception("EL username es invalido");
    }
    var user = await this._unitOfWork.Repository.UserRepository.SingleOrDefaultAsync(x => x.Username == username);
    var roles = await this._unitOfWork.Repository.RoleRepository.GetAllAsync();
    var collectorTypes = this._mapper.Map<IEnumerable<CollectorTypeDto>>(roles);

    var collector = new CollectorAdminViewDto();
    collector.id = user.Id;

    collector.username = user.Username;
    var centerList = (collector.collectorCenters ?? new List<BaseViewDto>()).ToList();
    var collectorList = (collector.collectors ?? new List<CollectorsWithCentersViewDto>()).ToList();

    int[] idUsers = (user?.IdUsers ?? "")
      .Split(',')
      .Select(id => int.TryParse(id, out int validId) ? validId : (int?)null)  // Devuelve el valor válido o null
      .Where(id => id.HasValue)  // Filtra solo los valores válidos
      .Select(id => id.Value)    // Convierte los valores nullable a enteros
      .ToArray();

    var usersBD = await this._unitOfWork.Repository.UserRepository.GetAllAsync(x => idUsers.Contains(x.Id));

    foreach (var userBD in usersBD ?? Enumerable.Empty<User>())
    {
      var collectorWithCenter = new CollectorsWithCentersViewDto();
      collectorWithCenter.id = userBD.Id;
      collectorWithCenter.name = userBD.Name;
      collectorWithCenter.collectorCenters = await collectorCenterByUser(userBD);
      collectorList.Add(collectorWithCenter);
    }
    collector.collectorCenters = await collectorCenterByUser(user);
    collector.collectors = collectorList.AsEnumerable();
    return collector;
  }

  private async Task<IEnumerable<BaseViewDto>> collectorCenterByUser(User user)
  {
    List<BaseViewDto> centerList = new List<BaseViewDto>();


    int[] idLocations = (user?.IdLocations ?? "")
      .Split(',')
      .Select(id => int.TryParse(id, out int validId) ? validId : (int?)null)  // Devuelve el valor válido o null
      .Where(id => id.HasValue)  // Filtra solo los valores válidos
      .Select(id => id.Value)    // Convierte los valores nullable a enteros
      .ToArray();
    var locations = await this._unitOfWork.Repository.LocationRepository.GetAllAsync(x => idLocations.Contains(x.Id));


    foreach (var location in locations ?? Enumerable.Empty<Location>())
    {
      var locationDetatil = new BaseViewDto();
      locationDetatil.id = location.Id;
      locationDetatil.name = location.Name;
      centerList.Add(locationDetatil);
    }
    return centerList.AsEnumerable();
  }

  public async Task<bool> UpdateCollector(UpdateCollector collector, int id)
  {
    try
    {
      var user = this._unitOfWork.Repository.UserRepository.SingleOrDefault(x => x.Id == id);

      if (collector.op == "replace")
      {
        switch (collector.path)
        {
          case "name":
            user.Name = collector.value;
            break;
          case "username":
            user.Username = collector.value;
            break;
          case "type":
            user.Type = collector.value;
            break;
          case "status":
            if (collector.value != "ACTIVE" || collector.value != "INACTIVE" || collector.value != "PENDING")
            {
              throw new Exception("El status no es valido para su asignación");
            }
            user.Status = collector.value;
            break;
          case "logo":
            user.Logo = collector.value;
            break;
          default:
            throw new ArgumentException("Propiedad no válida");
        }
      }
      this._unitOfWork.Repository.UserRepository.Update(user);
      await this._unitOfWork.SaveChangesAsync();
      return true;
    }
    catch (Exception error)
    {

      throw new Exception(error.Message);
    }

  }
}
