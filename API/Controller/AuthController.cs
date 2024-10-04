using API.Common.Enum;
using API.Common.Response;
using Application.AuthCore;
using Application.AuthCore.Dto;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;


[Route("api/v1/[controller]")]
[ApiController]
public class AuthController : ControllerBase 
{
  private readonly IAuthService _authService;
  public AuthController(
    IAuthService authService
  )
  {
    this._authService = authService;
  }

  [HttpPost()]
  public async Task<Response<RefreshTokenResult>> GenerateToken([FromBody] Credential credential)
  {
    try
    {
      var token = await this._authService.GenerateToken(credential);
      if (token == null)
      {
        throw new Exception("usuario no autorizado");
      }

      return  new Response<RefreshTokenResult>(
        StatusServerResponse.OK,
        token,
        true,
        ""
      );
    }
    catch (Exception error)
    {
     throw new Exception(error.Message);
    }
  }
  
  [HttpPost("refresh")]
  public async Task<Response<RefreshTokenResult>> RefreshToken()
  {

    // Obtener el token JWT del header de autorización
    var authHeader = Request.Headers["Authorization"].ToString();

    // Asegurarse de que el header contenga el token y que comience con "Bearer "
    if (authHeader == null)
    {
      throw new UnauthorizedAccessException("no se encontró el token en la petición");
    }
    var token = authHeader.Substring("Bearer ".Length).Trim();


    var refreshToken = await this._authService.GenerateRefreshToken(token);
      if (refreshToken == null)
    {
      throw new Exception("usuario no autorizado");
    }


    return  new Response<RefreshTokenResult>(
    StatusServerResponse.OK,
    refreshToken,
    true,
    ""
    );
  }

}