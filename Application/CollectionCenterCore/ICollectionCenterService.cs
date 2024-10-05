using System.Threading.Tasks;
using Application.CollectionCenterCore.Dto;

namespace Domain.CollectionCenter;

public interface ICollectionCenterService
{
  Task<IEnumerable<CollectionCenterDto>> GetAllCollectionCentersUseCase();
  Task<CollectionCenterDto> GetCollectionCenterByIdUseCase(int id);
}