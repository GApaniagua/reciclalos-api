using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Infrastructure.Persistence.Models;

namespace Infrastructure.Persistence.Mapping;

public class DepartamentoMapping: IEntityTypeConfiguration<Departamento>
{
  public void Configure(EntityTypeBuilder<Departamento> builder)
  {
  }
}