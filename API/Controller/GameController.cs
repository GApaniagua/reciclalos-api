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

    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IAPIResponseList<QuestionDTO>), contentTypes: ["application/json"])]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(IAPIResponseList<QuestionDTO>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, type: typeof(IAPIResponseList<QuestionDTO>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(IAPIResponseList<QuestionDTO>))]
    [SwaggerOperation(Summary = "Get Questions by game",
                    Description = "This service is public.")]
    public async Task<ActionResult<IAPIResponseList<QuestionDTO>>> GetQuestions(int id)
    {
        IAPIResponseList<QuestionDTO> apiResponse = new();

        if (id <= 0)
        {
            apiResponse.IsSuccess = false;
            apiResponse.Message = new List<string> { "id debe ser mayor a 0" };
            apiResponse.Status = HttpStatusCode.BadRequest;
            return BadRequest(apiResponse);
        }

        var defaultProperties = new List<string> { "Answers" };

        var dbResult = await questionRepository.GetListAllAsync(filter: $"GameId == {id}", tracked: false,
       includeProperties: string.Join(",", defaultProperties.ToArray()), filterFn: null);

        var result = mapper.Map<List<QuestionDTO>>(dbResult.List);

        apiResponse.Status = HttpStatusCode.OK;
        apiResponse.Data = result;
        apiResponse.Total = dbResult.Total;

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