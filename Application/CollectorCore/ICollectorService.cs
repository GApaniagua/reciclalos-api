using Application.CollectorCore.Dto;

namespace Domain.Collector;

public interface ICollectorService
{
  Task<IEnumerable<CollectorViewDto>> GetAllCollectorsUseCase();
  Task CreateCollector(CreateCollector collector);
  Task CreateCollectorWithMaterials(CreateCollectorWithMaterials collector);
  Task CreateCollectorSellMaterials(CreateCollectorSellMaterials collector);
  Task<CollectorAdminViewDto> GetAdminAllCollector(string token);
  Task<bool> UpdateCollector(UpdateCollector collector, int id);
  
}