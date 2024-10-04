using Infrastructure.Persistence.Interface;
using Infrastructure.Persistence.Models;


namespace Infrastructure.Persistence.Repository;

public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
{
  private readonly ReciclalosDbContext _DbContext;

  public QuestionRepository(
    ReciclalosDbContext context
  ) : base(context)
  {
    this._DbContext = context;
  }
}


