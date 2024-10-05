using Microsoft.AspNetCore.Mvc;
using Application.CollectorCore.Dto;
using Domain.Collector;
using API.Common.Response;
using API.Common.Enum;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace API.Controller;

[Route("api/v1/[controller]")]
[ApiController]
public class CollectorController : ControllerBase
{
  private readonly ICollectorService _collectorService;
  public CollectorController(
    ICollectorService collectorManager
  )
  {
    this._collectorService = collectorManager;
  }

  [Authorize(Policy = "AdminOnly")]
  [HttpGet()]
  public async Task<IEnumerable<CollectorViewDto>> GetCollector()
  {
    var response = await this._collectorService.GetAllCollectorsUseCase();
    return response;
  }

  [Authorize(Policy = "AdminOnly")]
  [HttpPost()]
  public async Task<Response<string>> CreateCollector([FromBody] CreateCollector collector)
  {
    try
    {
      await this._collectorService.CreateCollector(collector);
      return new Response<string>(
        HttpStatusCode.Created,
        "guadado con exito",
        true,
        ""
      );
    }
    catch (Exception error)
    {
      var errorResponse = new Response<string>(
          HttpStatusCode.InternalServerError,
          "Error en el servidor",
          false,
          $"{error.Message}"
      );

      return errorResponse;
    }
  }

  // ACOPIADOR Y GESTOR ACOPIADOR
  [Authorize(Roles = "U,E,A")]
  [HttpGet("assignments")]
  public async Task<Response<CollectorAdminViewDto>> GetCollectorAdmin()
  {
    try
    {
      // Verifica si el encabezado Authorization existe
      var authHeader = Request.Headers["Authorization"].FirstOrDefault();

      if (authHeader == null || !authHeader.StartsWith("Bearer "))
      {
        throw new Exception("no existe token en la petición");
      }
      var token = authHeader.Substring("Bearer ".Length).Trim();
      var collector = await this._collectorService.GetAdminAllCollector(token);
      return new Response<CollectorAdminViewDto>(
        HttpStatusCode.Created,
        collector,
        true,
        ""
      );
    }
    catch (Exception error)
    {
      throw new Exception(error.Message);
    }
  }

  [Authorize(Policy = "AdminOnly")]
  [HttpPatch("{id}")]
  public async Task<Response<bool>> UpdateColletor([FromBody] UpdateCollector collector, int id)
  {
    try
    {
      var collectors = await this._collectorService.UpdateCollector(collector, id);
      return new Response<bool>(
        HttpStatusCode.Created,
        collectors,
        true,
        ""
      );
    }
    catch (Exception error)
    {
      throw new Exception(error.Message);
    }
  }

  [HttpPost("collector/material")]
  public async Task<Response<string>> CreateCollectorWithMaterials([FromBody] CreateCollectorWithMaterials collector)
  {
    try
    {

      await this._collectorService.CreateCollectorWithMaterials(collector);
      return new Response<string>(
        HttpStatusCode.Created,
        "guadado con exito",
        true,
        ""
      );
    }
    catch (Exception error)
    {
      var errorResponse = new Response<string>(
          HttpStatusCode.InternalServerError,
          "Error en el servidor",
          false,
          $"{error.Message}"
      );

      return errorResponse;
    }
  }
  [HttpPost("collector/material/sell")]
  public async Task<Response<string>> CreateCollectorSellMaterials([FromBody] CreateCollectorSellMaterials collector)
  {
    try
    {
      await this._collectorService.CreateCollectorSellMaterials(collector);
      return new Response<string>(
        HttpStatusCode.Created,
        "guadado con exito",
        true,
        ""
      );
    }
    catch (Exception error)
    {
      var errorResponse = new Response<string>(
          HttpStatusCode.InternalServerError,
          "Error en el servidor",
          false,
          $"{error.Message}"
      );

      return errorResponse;
    }
  }
}