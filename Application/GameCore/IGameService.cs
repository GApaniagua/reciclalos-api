namespace Application.GameCore;

public interface IGameService
{
  Task<QuestionDTO> GetQuestionWithAnswersByIdUseCase(int id);
}