using Infrastructure.Persistence.Interface;
using Infrastructure.Persistence.Models;


namespace Infrastructure.Persistence.Repository;

public class AnswerRepository : GenericRepository<Answer>, IAnswerRepository
{
  private readonly ReciclalosDbContext _DbContext;

  public AnswerRepository(
    ReciclalosDbContext context
  ) : base(context)
  {
    this._DbContext = context;
  }
}


