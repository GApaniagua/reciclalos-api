namespace Application.CollectorCore.Dto;

public class CollectorAdminViewDto
{
    public int id { get; set; }
    public string username { get; set; }
    public IEnumerable<CollectorsWithCentersViewDto> collectors { get; set; } = Enumerable.Empty<CollectorsWithCentersViewDto>();
    public IEnumerable<BaseViewDto> collectorCenters { get; set; } = Enumerable.Empty<BaseViewDto>();

}