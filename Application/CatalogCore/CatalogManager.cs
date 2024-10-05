using AutoMapper;
using Infrastructure.Persistence.Interface;
using Domain.Catalog;
using Application.CatalogCore.Dto;
using Domain.Material;
using System.Text;
using Infrastructure.Persistence.Models;
using Application.CollectorCore.Dto;

namespace Application.CatalogCore;

public class CatalogManager : ICatalogService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public CatalogManager(IUnitOfWork unitOfWork, IMapper mapper)
  {
    this._unitOfWork = unitOfWork;
    this._mapper = mapper;
  }

  public async Task<IEnumerable<MaterialTypes>> GetAllMaterialTypesUseCase()
  {
    try
    {
      var materials = await this._unitOfWork.Repository.MaterialRepository.GetAllAsync();
      var materialsType = this._mapper.Map<IEnumerable<MaterialTypes>>(materials);
      return materialsType;
    }
    catch (Exception)
    {
      throw;
    }
  }


  public async Task<IEnumerable<MaterialRecyclableType>> GetAllMaterialRecyclableTypesUseCase()
  {
    try
    {
      var collections = await this._unitOfWork.Repository.CollectionRepository.GetAllAsync();
      var collectionsMap = _mapper.Map<IEnumerable<Catalog>>(collections);
      var MaterialReciclableType = _mapper.Map<IEnumerable<MaterialRecyclableType>>(collectionsMap);
      return MaterialReciclableType;
    }
    catch (Exception)
    {
      throw;
    }
  }

  public async Task<IEnumerable<Department>> GetAllDepartmentsUseCase()
  {
    try
    {
      var departamentos = await this._unitOfWork.Repository.DepartamentoRepository.GetAllAsync();
      var departments = _mapper.Map<IEnumerable<Department>>(departamentos);
      return departments;
    }
    catch (Exception)
    {
      throw;
    }
  }

  public async Task<IEnumerable<Municipality>> GetAllMunicipalitiesUseCase()
  {
    try
    {
      var municipios = await this._unitOfWork.Repository.MunicipioRepository.GetAllAsync();
      var municipalities = _mapper.Map<IEnumerable<Municipality>>(municipios);
      return municipalities;
    }
    catch (Exception)
    {
      throw;
    }
  }

  public async Task<string> GetReportUseCase(int? materialTypeId, DateTime start, DateTime end)
  {
    try
    {
      if (start > end)
      {
        throw new ArgumentException("La fecha de inicio debe ser anterior o igual a la fecha de fin.");
      }

      if (start == DateTime.MinValue || end == DateTime.MinValue)
      {
        throw new ArgumentException("Las fechas de inicio y fin no pueden ser nulas.");
      }
      if (materialTypeId != null)
      {

        var collections = await this._unitOfWork.Repository.CollectionRepository.GetAllAsync(x => x.Created >= start && x.Created <= end);

        var reportDtos = this._mapper.Map<IEnumerable<ReportDto>>(collections);
        foreach (var report in reportDtos)
        {
          var location = await this._unitOfWork.Repository.LocationRepository.SingleOrDefaultAsync(x => x.Id == report.IdCentroAcopio);
          report.CentroDeAcopio = location.Name;
        }

        var material = await this._unitOfWork.Repository.MaterialRepository.SingleOrDefaultAsync(x => x.Id == materialTypeId);
        if (material == null)
        {
          throw new Exception("Material no encontrado");
        }
        var materialMap = this._mapper.Map<MaterialTypes>(material);
        return GetGroupedByMaterial(material, materialMap, reportDtos);

      }
      else
      {

        var collections = await this._unitOfWork.Repository.CollectionRepository.GetAllAsync(x => x.Created >= start && x.Created <= end);
        var reportDtos = this._mapper.Map<IEnumerable<ReportDto>>(collections);
        foreach (var report in reportDtos)
        {
          var location = await this._unitOfWork.Repository.LocationRepository.SingleOrDefaultAsync(x => x.Id == report.IdCentroAcopio);
          report.CentroDeAcopio = location.Name;
        }

        var groupedReports = reportDtos
            .GroupBy(r => new { r.CentroDeAcopio, year = r.Fecha.Year, month = r.Fecha.Month, day = r.Fecha.Day })
            .Select(g => new ReportDto
            {
              CentroDeAcopio = g.Key.CentroDeAcopio,
              Latas = g.Sum(r => r.Latas),
              PapelYCarton = g.Sum(r => r.PapelYCarton),
              PlasticoPet = g.Sum(r => r.PlasticoPet),
              PlasticoOtros = g.Sum(r => r.PlasticoOtros),
              EnvasesVidrio = g.Sum(r => r.EnvasesVidrio),
              Fecha = new DateTime(g.Key.year, g.Key.month, g.Key.day), // Si la fecha es 0001-01-01, usar la actual
              TetraPak = g.Sum(r => r.TetraPak) // Usar la fecha más reciente
            }).ToList();

        var csvBuilder = new StringBuilder();

        // Añadir encabezados
        csvBuilder.AppendLine("CENTRO DE ACOPIO,LATAS,PAPEL Y CARTÓN,PLÁSTICO PET,PLÁSTICO OTROS,ENVASES Y BOTELLAS DE VIDRIO,TETRA PAK,FECHA");

        foreach (var registro in groupedReports)
        {
          csvBuilder.AppendLine($"{registro.CentroDeAcopio},{registro.Latas},{registro.PapelYCarton},{registro.PlasticoPet},{registro.PlasticoOtros},{registro.EnvasesVidrio},{registro.TetraPak},{registro.Fecha:yyyy-MM-dd}");
        }

        return csvBuilder.ToString();
      }
    }
    catch (Exception error)
    {

      throw new Exception(error.Message);
    }


  }

  private static string GetGroupedByMaterial(Material material, MaterialTypes materialMap, IEnumerable<ReportDto> reportDtos)
  {
    var csvBuilder = new StringBuilder();
    switch (materialMap.Name)
    {
      case "Latas":
        reportDtos
          .GroupBy(r => new { r.CentroDeAcopio, r.Fecha })
          .Select(g => new ReportDto
          {
            CentroDeAcopio = g.Key.CentroDeAcopio,
            Latas = g.Sum(r => r.Latas),
            Fecha = g.Key.Fecha != DateTime.MinValue ? g.Key.Fecha : DateTime.Now, // Si la fecha es 0001-01-01, usar la actual
          }).ToList();


        csvBuilder.AppendLine("CENTRO DE ACOPIO,LATAS,FECHA");

        foreach (var registro in reportDtos)
        {
          csvBuilder.AppendLine($"{registro.CentroDeAcopio},{registro.Latas},{registro.Fecha:yyyy-MM-dd}");
        }
        break;
      case "Papel y cartón":
        reportDtos
          .GroupBy(r => new { r.CentroDeAcopio, r.Fecha })
          .Select(g => new ReportDto
          {
            CentroDeAcopio = g.Key.CentroDeAcopio,
            PapelYCarton = g.Sum(r => r.PapelYCarton),
            Fecha = g.Key.Fecha != DateTime.MinValue ? g.Key.Fecha : DateTime.Now, // Si la fecha es 0001-01-01, usar la actual
          }).ToList();
        csvBuilder.AppendLine("CENTRO DE ACOPIO,PAPEL Y CARTON,FECHA");


        foreach (var registro in reportDtos)
        {
          csvBuilder.AppendLine($"{registro.CentroDeAcopio},{registro.PapelYCarton},{registro.Fecha:yyyy-MM-dd}");
        }
        break;
      case "Plasticos PET":
        reportDtos
          .GroupBy(r => new { r.CentroDeAcopio, r.Fecha })
          .Select(g => new ReportDto
          {
            CentroDeAcopio = g.Key.CentroDeAcopio,
            PlasticoPet = g.Sum(r => r.PlasticoPet),
            Fecha = g.Key.Fecha != DateTime.MinValue ? g.Key.Fecha : DateTime.Now, // Si la fecha es 0001-01-01, usar la actual
          }).ToList();
        csvBuilder.AppendLine("CENTRO DE ACOPIO,PLÁSTICO PET,FECHA");


        foreach (var registro in reportDtos)
        {
          csvBuilder.AppendLine($"{registro.CentroDeAcopio},{registro.PlasticoPet},{registro.Fecha:yyyy-MM-dd}");
        }
        break;
      case "Plasticos Otros":
        reportDtos
          .GroupBy(r => new { r.CentroDeAcopio, r.Fecha })
          .Select(g => new ReportDto
          {
            CentroDeAcopio = g.Key.CentroDeAcopio,
            PlasticoOtros = g.Sum(r => r.PlasticoOtros),
            Fecha = g.Key.Fecha != DateTime.MinValue ? g.Key.Fecha : DateTime.Now, // Si la fecha es 0001-01-01, usar la actual
          }).ToList();
        csvBuilder.AppendLine("CENTRO DE ACOPIO,PLASTICO OTROS,FECHA");


        foreach (var registro in reportDtos)
        {
          csvBuilder.AppendLine($"{registro.CentroDeAcopio},{registro.PlasticoOtros},{registro.Fecha:yyyy-MM-dd}");
        }

        break;
      case "Envases y botellas de vidrio":
        reportDtos
          .GroupBy(r => new { r.CentroDeAcopio, r.Fecha })
          .Select(g => new ReportDto
          {
            CentroDeAcopio = g.Key.CentroDeAcopio,
            EnvasesVidrio = g.Sum(r => r.EnvasesVidrio),
            Fecha = g.Key.Fecha != DateTime.MinValue ? g.Key.Fecha : DateTime.Now,
          }).ToList();
        csvBuilder.AppendLine("CENTRO DE ACOPIO,ENVASES Y BOTELLAS DE VIDRIO,FECHA");


        foreach (var registro in reportDtos)
        {
          csvBuilder.AppendLine($"{registro.CentroDeAcopio},{registro.EnvasesVidrio},{registro.Fecha:yyyy-MM-dd}");
        }

        break;
      case "Multicapa":
        reportDtos
          .GroupBy(r => new { r.CentroDeAcopio, r.Fecha })
          .Select(g => new ReportDto
          {
            CentroDeAcopio = g.Key.CentroDeAcopio,
            Fecha = g.Key.Fecha != DateTime.MinValue ? g.Key.Fecha : DateTime.Now, // Si la fecha es 0001-01-01, usar la actual
            TetraPak = g.Sum(r => r.TetraPak) // Usar la fecha más reciente
          }).ToList();
        csvBuilder.AppendLine("CENTRO DE ACOPIO,TETRA PAK,FECHA");


        foreach (var registro in reportDtos)
        {
          csvBuilder.AppendLine($"{registro.CentroDeAcopio},{registro.TetraPak},{registro.Fecha:yyyy-MM-dd}");
        }

        break;
      default:
        throw new Exception("Material no reconocido: " + material.Name);
        // break;
    }
    return csvBuilder.ToString();
  }

  public async Task<IEnumerable<CollectorTypeDto>> GetCollectorTypeUseCase()
  {
    var roles = await this._unitOfWork.Repository.RoleRepository.GetAllAsync();
    var collectorType = this._mapper.Map<IEnumerable<CollectorTypeDto>>(roles);
    return collectorType;
  }
}