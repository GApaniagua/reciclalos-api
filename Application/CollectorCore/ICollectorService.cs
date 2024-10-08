using Application.CollectorCore.Dto;

namespace Domain.Collector;

public interface ICollectorService
{
  Task<IEnumerable<CollectorViewDto>> GetAllCollectorsUseCase();
  Task CreateCollector(CreateCollector collector);
  Task CreateCollectorWithMaterials(CreateCollectorWithMaterialsDTO collector);
  Task CreateCollectorSellMaterials(CreateCollectorSellMaterialsDTO collector);
  Task<CollectorAdminViewDto> GetAdminAllCollector(string token);
  Task<bool> UpdateCollector(UpdateCollector collector, int id);

}