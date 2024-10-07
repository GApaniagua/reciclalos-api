using AutoMapper;
using Infrastructure.Persistence.Interface;


namespace Application.GameCore;

public class GameManager : IGameService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public GameManager(IUnitOfWork unitOfWork, IMapper mapper)
  {
    this._unitOfWork = unitOfWork;
    this._mapper = mapper;
  }

  public async Task<QuestionDTO> GetQuestionWithAnswersByIdUseCase(int id)
  {
    try
    {
      var question = await this._unitOfWork.Repository.QuestionRepository.SingleOrDefaultAsync(x => x.Id == id);
      if (question == null)
      {
        throw new Exception($"id inválido, no se encuentran preguntas con {id}");
      }
      var questionsMap = this._mapper.Map<QuestionDTO>(question);
      var answers = await this._unitOfWork.Repository.AnswerRepository.GetAllAsync(x => x.QuestionId == id);
      var answersMap = this._mapper.Map<IEnumerable<AnswerDto>>(answers);
      questionsMap.Answers = answersMap.ToList();
      return questionsMap;
    }
    catch (Exception error)
    {
      throw new Exception(error.Message);
    }
  }
}