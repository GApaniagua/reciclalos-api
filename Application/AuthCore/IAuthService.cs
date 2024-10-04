using System.Security.Claims;
using Application.AuthCore.Dto;
using Infrastructure.Persistence.Models;

namespace Application.AuthCore;
public interface IAuthService
{
    Task<RefreshTokenResult> GenerateToken(Credential credential);
    Task<RefreshTokenResult> GenerateRefreshToken(string token);
    ClaimsPrincipal  GetPrincipalFromExpiredToken(string token);
}
