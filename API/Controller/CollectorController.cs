using Microsoft.AspNetCore.Mvc;
using Application.CollectorCore.Dto;
using Domain.Collector;
using API.Common.Response;
using API.Common.Enum;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Swashbuckle.AspNetCore.Annotations;
using Infrastructure.Persistence.Interface;
using Infrastructure.Persistence.Models;
using AutoMapper;

namespace API.Controller;

[Route("api/v1/[controller]")]
[ApiController]
public class CollectorController : ControllerBase
{
  private readonly ICollectorService _collectorService;
  private readonly ILocationRepository locationRepository;
  private readonly IUserRepository userRepository;
  private readonly ICollectionRepository collectionRepository;
  private readonly IMapper mapper;

  public CollectorController(ICollectorService collectorManager, ILocationRepository locationRepository, IUserRepository userRepository,
ICollectionRepository collectionRepository, IMapper mapper)
  {
    this._collectorService = collectorManager;
    this.locationRepository = locationRepository;
    this.userRepository = userRepository;
    this.collectionRepository = collectionRepository;
    this.mapper = mapper;
  }

  [Authorize(Policy = "AdminOnly")]
  [HttpGet()]
  public async Task<IEnumerable<CollectorViewDto>> GetCollector()
  {
    var response = await this._collectorService.GetAllCollectorsUseCase();
    return response;
  }

  [Authorize(Policy = "AdminOnly")]
  [HttpPost()]
  public async Task<Response<string>> CreateCollector([FromBody] CreateCollector collector)
  {
    try
    {
      await this._collectorService.CreateCollector(collector);
      return new Response<string>(
        HttpStatusCode.Created,
        "guadado con exito",
        true,
        ""
      );
    }
    catch (Exception error)
    {
      var errorResponse = new Response<string>(
          HttpStatusCode.InternalServerError,
          "Error en el servidor",
          false,
          $"{error.Message}"
      );

      return errorResponse;
    }
  }

  // ACOPIADOR Y GESTOR ACOPIADOR
  [Authorize(Roles = "U,E,A")]
  [HttpGet("assignments")]
  public async Task<Response<CollectorAdminViewDto>> GetCollectorAdmin()
  {
    try
    {
      // Verifica si el encabezado Authorization existe
      var authHeader = Request.Headers["Authorization"].FirstOrDefault();

      if (authHeader == null || !authHeader.StartsWith("Bearer "))
      {
        throw new Exception("no existe token en la petición");
      }
      var token = authHeader.Substring("Bearer ".Length).Trim();
      var collector = await this._collectorService.GetAdminAllCollector(token);
      return new Response<CollectorAdminViewDto>(
        HttpStatusCode.Created,
        collector,
        true,
        ""
      );
    }
    catch (Exception error)
    {
      throw new Exception(error.Message);
    }
  }

  [Authorize(Policy = "AdminOnly")]
  [HttpPatch("{id}")]
  public async Task<Response<bool>> UpdateColletor([FromBody] UpdateCollector collector, int id)
  {
    try
    {
      var collectors = await this._collectorService.UpdateCollector(collector, id);
      return new Response<bool>(
        HttpStatusCode.Created,
        collectors,
        true,
        ""
      );
    }
    catch (Exception error)
    {
      throw new Exception(error.Message);
    }
  }

  [HttpPost("collector/material")]
  [SwaggerResponse(StatusCodes.Status201Created, type: typeof(IAPIResponse<CreateCollectorWithMaterialsDTO>), contentTypes: ["application/json"])]
  [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(IAPIResponse<CreateCollectorWithMaterialsDTO>))]
  [SwaggerResponse(StatusCodes.Status401Unauthorized, type: typeof(IAPIResponse<CreateCollectorWithMaterialsDTO>))]
  [SwaggerResponse(StatusCodes.Status404NotFound, type: typeof(IAPIResponse<CreateCollectorWithMaterialsDTO>))]
  [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(IAPIResponse<CreateCollectorWithMaterialsDTO>))]
  public async Task<ActionResult<IAPIResponse<CreateCollectorWithMaterialsDTO>>> CreateCollectorWithMaterials([FromBody] CreateCollectorWithMaterialsDTO collector)
  {
    IAPIResponse<CreateCollectorWithMaterialsDTO> apiResponse = new();

    if (collector.CollectorId <= 0 || collector.CollectionCenterId <= 0)
    {
      apiResponse.IsSuccess = false;
      apiResponse.Message = new List<string> { "CollectorId o CollectionCenterId debe ser mayor a 0" };
      apiResponse.Status = HttpStatusCode.BadRequest;
      return BadRequest(apiResponse);
    }

    var collectorId = collector.CollectorId;
    var collectionCenterId = collector.CollectionCenterId;
    var materials = collector.Materials;
    var location = await locationRepository.GetAsync($"Id == {collectionCenterId}");
    var user = await userRepository.GetAsync($"Id == {collectorId}");

    if (location == null || user == null || (user != null && user.Type != "U"))
    {
      if (apiResponse.Message == null)
        apiResponse.Message = new List<string>();

      if (location == null)
      {
        apiResponse.Message.Add("CollectionCenterId es inválido");
      }
      if (user == null)
      {
        apiResponse.Message.Add("CollectorId es inválido");
      }
      if (user != null && user.Type != "U")
      {
        apiResponse.Message.Add("Rol del recolector es inválido");
      }

      apiResponse.Status = HttpStatusCode.NotFound;
      apiResponse.IsSuccess = false;
      return NotFound(apiResponse);
    }


    var collection = await collectionRepository.GetAsync(filter: $"IdUser == {(short)collectorId} And IdLocation == {(short)collectionCenterId}");
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
          if (apiResponse.Message == null)
            apiResponse.Message = new List<string>();

          apiResponse.Message.Add($"{material.MaterialType} material inválido.");
          break;
      }
    }

    if (isUpdate)
    {
      collection.DateUpdated = DateTime.Now;
      collection = await collectionRepository.UpdateAsync(collection);
    }
    else
    {
      await collectionRepository.AddAsync(collection);
    }

    var result = mapper.Map<CreateCollectorWithMaterialsDTO>(collection);
    result.Materials = new List<MaterialsCollectorDTO>();

    foreach (var material in materials)
    {
      if (material.MaterialType == "Latas" || material.MaterialType == "Papel y cartón" || material.MaterialType == "Plasticos PET" || material.MaterialType == "Plasticos Otros"
      || material.MaterialType == "Envases y botellas de vidrio" || material.MaterialType == "Multicapa")
      {
        result.Materials.Add(new MaterialsCollectorDTO { MaterialType = material.MaterialType, Quantity = material.Quantity });
      }
    }

    apiResponse.Status = isUpdate ? HttpStatusCode.OK : HttpStatusCode.Created;
    apiResponse.Data = result;

    return isUpdate ? Ok(apiResponse) : CreatedAtRoute("", null, apiResponse);

    // await this._unitOfWork.SaveChangesAsync();

    // await this._collectorService.CreateCollectorWithMaterials(collector);
    // return new IAPIResponse<string>(
    //   HttpStatusCode.Created,
    //   "guadado con exito",
    //   true,
    //   ""
    // );

  }
  [HttpPost("collector/material/sell")]
  public async Task<Response<string>> CreateCollectorSellMaterials([FromBody] CreateCollectorSellMaterialsDTO collector)
  {
    try
    {
      await this._collectorService.CreateCollectorSellMaterials(collector);
      return new Response<string>(
        HttpStatusCode.Created,
        "guadado con exito",
        true,
        ""
      );
    }
    catch (Exception error)
    {
      var errorResponse = new Response<string>(
          HttpStatusCode.InternalServerError,
          "Error en el servidor",
          false,
          $"{error.Message}"
      );

      return errorResponse;
    }
  }
}