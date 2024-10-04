using Infrastructure.Persistence.Interface;
using Infrastructure.Persistence.Models;


namespace Infrastructure.Persistence.Repository;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
  private readonly ReciclalosDbContext _DbContext;

  public RoleRepository(
    ReciclalosDbContext context
  ) : base(context)
  {
    this._DbContext = context;
  }
}


