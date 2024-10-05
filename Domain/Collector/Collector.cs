namespace Domain.Collector;

public class Collector
{
  public int id { get; private set; }
  public string name { get; private set; } = string.Empty;
  public string username { get; private set; } = string.Empty;
  public string email { get; private set; } = string.Empty;
  public string password { get; private set; } = string.Empty;
  public string type { get; private set; } = string.Empty;
  public string IdLocations { get; private set; } = string.Empty;
  public string idUsers { get; private set; } = string.Empty;
  public string logo { get; private set; } = string.Empty;
  public string status { get; set; } = string.Empty;
  public DateTime created { get; set; }

}