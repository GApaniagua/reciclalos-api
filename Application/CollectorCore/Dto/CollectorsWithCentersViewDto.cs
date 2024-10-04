namespace Application.CollectorCore.Dto;

public class CollectorsWithCentersViewDto : BaseViewDto
{
    // public CollectorTypeDto? type { get; set; }
    public IEnumerable<BaseViewDto> collectorCenters { get; set; }
}