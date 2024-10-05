using Infrastructure.Persistence.Interface;
using Infrastructure.Persistence.Models;


namespace Infrastructure.Persistence.Repository;

public class GameRepository : GenericRepository<Game>, IGameRepository
{
  private readonly ReciclalosDbContext _DbContext;

  public GameRepository(
    ReciclalosDbContext context
  ) : base(context)
  {
    this._DbContext = context;
  }
}


