using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Infrastructure.Persistence.Models;

namespace Infrastructure.Persistence.Mapping;

public class MunicipioMapping: IEntityTypeConfiguration<Municipio>
{
  public void Configure(EntityTypeBuilder<Municipio> builder)
  {
  }
}