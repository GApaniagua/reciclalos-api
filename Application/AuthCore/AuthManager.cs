using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.AuthCore.Dto;
using Application.Common;
using Infrastructure.Persistence.Interface;
using Infrastructure.Persistence.Models;
using Microsoft.IdentityModel.Tokens;

namespace Application.AuthCore;

public class AuthManager : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
  
    private readonly TimeSpan _refreshTokenLifetime = TimeSpan.FromDays(double.Parse(Environment.GetEnvironmentVariable("EXPIRATION_TOKEN"))); // Duración del refresh token
    private readonly TimeSpan _tokenLifetime = TimeSpan.FromDays(double.Parse(Environment.GetEnvironmentVariable("EXPIRATION_REFRESH_TOKEN"))); // Duración del refresh token

    public AuthManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<RefreshTokenResult> GenerateToken(Credential credential)
    {
        try
        {
            User user = await ValidateCredentials(credential);
            var refreshTokenExpiration = DateTime.UtcNow.Add(_refreshTokenLifetime);
            var accessTokenExpiration = DateTime.UtcNow.Add(_refreshTokenLifetime);
            await RegisterDeviceLogin(credential);
            return new RefreshTokenResult
            {
                accessToken = MakeToken(user, _refreshTokenLifetime),
                refreshToken = MakeToken(user, _refreshTokenLifetime),
                accessTokenExpireOn = accessTokenExpiration,
                refreshTokenExpireOn = refreshTokenExpiration,
            };
        }
        catch (Exception error)
        {
            
            throw new Exception($"Error al generar el token: {error.Message}");
        }
    }

    private async Task RegisterDeviceLogin(Credential credential)
    {
        try
        {
            var registerLogin = new UserDeviceLogin();
            registerLogin.Device = credential.device;
            registerLogin.AppVersion = credential.appVersion;
            registerLogin.Os = credential.os;
            registerLogin.CreatedAt = DateTime.Now;
            registerLogin.Username = credential.username;
            registerLogin.Password = credential.password;

            await this._unitOfWork.Repository.UserDeviceLoginRepository.AddAsync(registerLogin);
            await this._unitOfWork.SaveChangesAsync();
        }
        catch (Exception error)
        { 
            throw new Exception($"Error al guardar registros del dispositivo: {error.Message}");
        }
    }

    public async Task<RefreshTokenResult> GenerateRefreshToken(string tokenForRefresh)
    {
        try
        {
            var claimsFromToken = GetPrincipalFromExpiredToken(tokenForRefresh);
            var username = claimsFromToken.FindFirst("username")?.Value;
            Credential credential = new Credential();
            credential.username = username ?? throw new UnauthorizedAccessException("El usuario es invalido");
            var user = await ValidateCredentials(credential, true);
            var refreshTokenExpiration = DateTime.UtcNow.Add(_refreshTokenLifetime);
            var accessTokenExpiration = DateTime.UtcNow.Add(_tokenLifetime);
            return new RefreshTokenResult
            {
                accessToken = MakeToken(user, _tokenLifetime),
                refreshToken = MakeToken(user, _refreshTokenLifetime),
                accessTokenExpireOn = accessTokenExpiration,
                refreshTokenExpireOn = refreshTokenExpiration,
            };
        }
        catch (Exception error)
        {
            throw new Exception($"Error al guardar registros del dispositivo: {error.Message}");               
        }
    }


    private string MakeToken(User user, TimeSpan  tokenLifeTime)
    {
        Claim[] claims = GenerateClaims(user);

        var tokenKey = Environment.GetEnvironmentVariable("TOKEN_KEY");
        if (string.IsNullOrEmpty(tokenKey))
        {
            throw new Exception("No se cargo la variable de entorno para la llave del token");
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


        var token = new JwtSecurityToken(
            issuer: Environment.GetEnvironmentVariable("TOKEN_KEY"),
            audience: Environment.GetEnvironmentVariable("TOKEN_KEY"),
            claims: claims,
            expires: DateTime.UtcNow.Add(tokenLifeTime),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static Claim[] GenerateClaims(User user)
    {
        return new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim("id", user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("role", user.Type),
            new Claim("username", user.Username),
        };
    }

    public async Task<User> ValidateCredentials(Credential credential, bool isRefresh = false)
    {
        var user = await _unitOfWork.Repository.UserRepository.FirstAsync(X => X.Username == credential.username);
        
        if (user == null)
        {
            throw new Exception("nombre de usuario inválida");
        }

        if (user.Status != "ACTIVE")
        {
            throw new Exception($"usuario no puede loguearse, ya que se encuentra con el siguiente estado: {user.Status}");
        }
        if (!isRefresh) 
        {
            var encryptor = new PasswordEncryptor();
            if (!encryptor.VerifyPassword(credential.password, user.Password))
            {
                throw new Exception("contraseña inválida");
            }
        } 
        return user;
    }

    

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        try
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("TOKEN_KEY"))), // Clave secreta para firmar el token
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false, // Aquí no validamos la expiración del token
                ClockSkew = TimeSpan.Zero // Sin tolerancia en el tiempo
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;


            // Intentamos validar el token y obtener los claims
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtToken = (JwtSecurityToken)securityToken;
            if (jwtToken == null)
            {
                return null; // Token no es válido
            }

            return principal; // Devolvemos los claims del token
        }
        catch
        {
            return null; // Si falla, el token no es válido
        }
    }

    // public bool IsRefreshTokenExpired(ClaimsPrincipal principal)
    // {
    //     var expClaim = principal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp);
        
    //     if (expClaim != null && long.TryParse(expClaim.Value, out long exp))
    //     {
    //         var expirationDate = DateTimeOffset.FromUnixTimeSeconds(exp).UtcDateTime;
    //         return DateTime.UtcNow >= expirationDate;
    //     }

    //     return true; // Si no hay claim de expiración, consideramos el token como inválido
    // }

}