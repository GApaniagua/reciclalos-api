using Infrastructure.Persistence.Interface;
using Infrastructure.Persistence.Models;


namespace Infrastructure.Persistence.Repository;

public class UserDeviceLoginRepository : GenericRepository<UserDeviceLogin>, IUserDeviceLoginRepository
{
  private readonly ReciclalosDbContext _DbContext;

  public UserDeviceLoginRepository(
    ReciclalosDbContext context
  ) : base(context)
  {
    this._DbContext = context;
  }
}


