using Microsoft.AspNetCore.Mvc;
using Application.CollectionCenterCore.Dto;
using Domain.CollectionCenter;
using API.Common.Response;
using API.Common.Enum;
using AutoMapper;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Sprache;


namespace API.Controller;

[Route("api/v1/[controller]")]
[ApiController]
public class CollectionCenterController : ControllerBase
{
    private readonly ICollectionCenterService _collectorService;
    public CollectionCenterController(
      ICollectionCenterService collectorManager
    )
    {
        this._collectorService = collectorManager;
    }

    [HttpGet()]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(Response<CollectionCenterDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, type: typeof(Response<CollectionCenterDto>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(Response<CollectionCenterDto>))]
    [SwaggerOperation(Summary = "Obtains Collector Center",
                  Description = "This service is public.")]
    public async Task<ActionResult<Response<IEnumerable<CollectionCenterDto>>>> GetCollectionCenter()
    {
        Response<IEnumerable<CollectionCenterDto>> response = new();

        var result = await this._collectorService.GetAllCollectionCentersUseCase();

        if (result == null)
        {
            response.Status = HttpStatusCode.NotFound;
            response.Data = null;
            response.Message = $"No collections centers was found";
            return NotFound(response);
        }

        response.Data = result;
        response.Status = HttpStatusCode.OK;
        return Ok(response);
    }

    [HttpGet("{id}", Name = "GetCollectorsCentersById")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(Response<CollectionCenterDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, type: typeof(Response<CollectionCenterDto>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(Response<CollectionCenterDto>))]
    [SwaggerOperation(Summary = "Obtains Collector Center by id",
                  Description = "This service is public.")]
    public async Task<ActionResult<Response<CollectionCenterDto>>> GetCollectionCenterById(int id)
    {
        Response<CollectionCenterDto> response = new();

        if (id <= 0)
        {
            response.Status = HttpStatusCode.BadRequest;
            response.Data = null;
            response.Message = $"The id: {id} is invalid";
            return BadRequest(response);
        }

        var result = await this._collectorService.GetCollectionCenterByIdUseCase(id);

        if (result == null)
        {
            response.Status = HttpStatusCode.NotFound;
            response.Data = null;
            response.Message = $"No collection center was found with id: {id}";
            return NotFound(response);
        }

        response.Data = result;
        response.Status = HttpStatusCode.OK;
        return Ok(response);
    }
}