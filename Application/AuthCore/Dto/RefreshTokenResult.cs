namespace Application.AuthCore.Dto;

public class RefreshTokenResult
{
    public string accessToken { get; set; }
    public string refreshToken { get; set; }
    public DateTime accessTokenExpireOn { get; set; }
    public DateTime refreshTokenExpireOn { get; set; }

}