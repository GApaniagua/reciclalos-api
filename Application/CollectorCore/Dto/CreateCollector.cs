namespace Application.CollectorCore.Dto;

public class CreateCollector {
  public  string Name { get; set;} = null!;
  public string Username  { get; set;} = null!;
  public string Password  { get; set;} = null!;
  public string CollectorTypeId  { get; set;} = null!;
  public string? CollectorCentersIds  { get; set;} = null!;
  public string? CollectorsIds  { get; set;} = null!;
  public string Image   { get; set;} = null!;
  }