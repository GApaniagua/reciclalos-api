namespace Application.AuthCore.Dto;

public class Credential
{
    public string username { get; set; }
    public string? password { get; set; }
    public string?  device { get; set; }
    public string? os { get; set; }
    public string? appVersion { get; set; }

}