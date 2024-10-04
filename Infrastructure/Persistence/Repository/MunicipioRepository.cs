using Infrastructure.Persistence.Interface;
using Infrastructure.Persistence.Models;


namespace Infrastructure.Persistence.Repository;

public class MunicipioRepository : GenericRepository<Municipio>, IMunicipioRepository
{
  private readonly ReciclalosDbContext _DbContext;

  public MunicipioRepository(
    ReciclalosDbContext context
  ) : base(context)
  {
    this._DbContext = context;
  }
}


