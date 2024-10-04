using System.Linq;
using Infrastructure.Persistence.Interface;
using Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence.Repository;

public class LocationRepository : GenericRepository<Location>, ILocationRepository
{
  private readonly ReciclalosDbContext _DbContext;

  public LocationRepository(
    ReciclalosDbContext context
  ) : base(context)
  {
    this._DbContext = context;
  }
}


