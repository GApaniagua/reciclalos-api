using System.Net;
using API.Common.Enum;
using API.Common.Response;
using Application.CollectionCenterCore.Dto;
using Application.GameCore;
using AutoMapper;
using Infrastructure.Persistence.Interface;

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controller;


[Route("api/v1/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IQuestionRepository questionRepository;

    private readonly IGameRepository gameRepository;
    private readonly IMapper mapper;

    public GameController(
      IQuestionRepository questionRepository, IGameRepository gameRepository, IMapper mapper
    )
    {
        this.questionRepository = questionRepository;
        this.gameRepository = gameRepository;
        this.mapper = mapper;
    }

    [HttpGet("{id}/questions")]

    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(IAPIResponse<GameViewDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, type: typeof(IAPIResponse<GameViewDto>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(IAPIResponse<GameViewDto>))]
    [SwaggerOperation(Summary = "Get Questions by game",
                    Description = "This service is public.")]
    public async Task<ActionResult<Response<GameViewDto>>> GetQuestions(int id)
    {
        IAPIResponse<GameViewDto> apiResponse = new();

        if (id <= 0)
        {
            apiResponse.IsSuccess = false;
            apiResponse.Message = new List<string> { "id must be greater than 0" };
            apiResponse.Status = HttpStatusCode.BadRequest;
            return BadRequest(apiResponse);
        }

        var defaultProperties = new List<string>();

        var dbResult = await questionRepository.GetAsync(filter: $"Id == {id}", tracked: false,
       includeProperties: string.Join(",", defaultProperties.ToArray()), filterFn: null);

        if (dbResult == null)
        {
            apiResponse.IsSuccess = false;
            apiResponse.Message = new List<string> { $"Flow {id} not found" };
            apiResponse.Status = HttpStatusCode.NotFound;
            return NotFound(apiResponse);
        }

        var result = mapper.Map<GameViewDto>(dbResult);
        apiResponse.Status = HttpStatusCode.OK;
        apiResponse.Data = result;

        return Ok(apiResponse);

        // try
        // {
        //     var questions = await this._gameService.GetQuestionWithAnswersByIdUseCase(id);
        //     return new Response<GameViewDto>(
        //     HttpStatusCode.OK,
        //     questions,
        //     true,
        //     ""
        //     );
        // }
        // catch (Exception error)
        // {
        //     throw new Exception(error.Message);
        // }
    }

}