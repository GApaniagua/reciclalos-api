namespace Application.CollectorCore.Dto;

public class CollectorViewDto : BaseViewDto
{

  public string username { get; private set; } = string.Empty;
  public string collectorTypeId { get; private set; } = string.Empty;
  public CollectorTypeDto type { get; set; }
  public string collectorCentersIds { get; private set; } = string.Empty;
  public string collectorCenter { get; private set; } = string.Empty;
  public string imageUrl { get; private set; } = string.Empty;
  public int statusId { get; private set; }
  public string status { get; private set; } = string.Empty;
}
