using Infrastructure.Persistence.Interface;
using Infrastructure.Persistence.Models;


namespace Infrastructure.Persistence.Repository;

public class DepartamentoRepository : GenericRepository<Departamento>, IDepartamentoRepository
{
  private readonly ReciclalosDbContext _DbContext;

  public DepartamentoRepository(
    ReciclalosDbContext context
  ) : base(context)
  {
    this._DbContext = context;
  }
}


