using Infrastructure.Persistence.Interface;
using Infrastructure.Persistence.Models;


namespace Infrastructure.Persistence.Repository;

public class MaterialRepository : GenericRepository<Material>, IMaterialRepository
{
  private readonly ReciclalosDbContext _DbContext;

  public MaterialRepository(
    ReciclalosDbContext context
  ) : base(context)
  {
    this._DbContext = context;
  }
}


