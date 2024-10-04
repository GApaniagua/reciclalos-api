using Infrastructure.Persistence.Interface;
using Infrastructure.Persistence.Models;


namespace Infrastructure.Persistence.Repository;

public class CollectionRepository : GenericRepository<Collection>, ICollectionRepository
{
  private readonly ReciclalosDbContext _DbContext;

  public CollectionRepository(
    ReciclalosDbContext context
  ) : base(context)
  {
    this._DbContext = context;
  }
}


