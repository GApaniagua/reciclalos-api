using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Infrastructure.Persistence.Models;

namespace Infrastructure.Persistence.Mapping;

public class LocationMapping: IEntityTypeConfiguration<Location>
{
  public void Configure(EntityTypeBuilder<Location> builder)
  {
  }
}