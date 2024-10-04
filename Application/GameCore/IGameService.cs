namespace Application.GameCore;

public interface IGameService
{
  Task<GameViewDto> GetQuestionWithAnswersByIdUseCase(int id);
}