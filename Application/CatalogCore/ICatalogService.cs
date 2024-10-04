using Application.CatalogCore.Dto;
using Application.CollectorCore.Dto;
using Domain.Material;

namespace Domain.Catalog;

public interface ICatalogService
{
  Task<IEnumerable<MaterialTypes>> GetAllMaterialTypesUseCase();
  Task<IEnumerable<MaterialRecyclableType>> GetAllMaterialRecyclableTypesUseCase();
  Task<IEnumerable<Department>> GetAllDepartmentsUseCase();
  Task<IEnumerable<Municipality>> GetAllMunicipalitiesUseCase();
  Task<string> GetReportUseCase(int? materialTypeId, DateTime start, DateTime end);
  Task<IEnumerable<CollectorTypeDto>> GetCollectorTypeUseCase();
}