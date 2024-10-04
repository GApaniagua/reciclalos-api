using Infrastructure.Persistence.Interface;
using Infrastructure.Persistence.Models;


namespace Infrastructure.Persistence.Repository;

public class UserRepository : GenericRepository<User>, IUserRepository
{
  private readonly ReciclalosDbContext _DbContext;

  public UserRepository(
    ReciclalosDbContext context
  ) : base(context)
  {
    this._DbContext = context;
  }
}


