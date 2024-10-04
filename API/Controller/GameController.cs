using API.Common.Enum;
using API.Common.Response;
using Application.CollectionCenterCore.Dto;
using Application.GameCore;

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controller;


[Route("api/v1/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;
    public GameController(
      IGameService gameService
    )
    {
        this._gameService = gameService;
    }

    [HttpGet("{id}/questions")]

    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(Response<GameViewDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, type: typeof(Response<GameViewDto>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(Response<GameViewDto>))]
    [SwaggerOperation(Summary = "Get Questions by game",
                    Description = "This service is public.")]
    public async Task<Response<GameViewDto>> GetQuestions(int id)
    {
        try
        {
            var questions = await this._gameService.GetQuestionWithAnswersByIdUseCase(id);
            return new Response<GameViewDto>(
            StatusServerResponse.OK,
            questions,
            true,
            ""
            );
        }
        catch (Exception error)
        {
            throw new Exception(error.Message);
        }
    }

}