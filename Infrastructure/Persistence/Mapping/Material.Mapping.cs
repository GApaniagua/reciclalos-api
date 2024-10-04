using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Infrastructure.Persistence.Models;

namespace Infrastructure.Persistence.Mapping;

public class MaterialMapping: IEntityTypeConfiguration<Material>
{
  public void Configure(EntityTypeBuilder<Material> builder)
  {
  }
}