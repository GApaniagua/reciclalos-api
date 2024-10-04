using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Infrastructure.Persistence.Models;

namespace Infrastructure.Persistence.Mapping;

public class CollectionMapping: IEntityTypeConfiguration<Collection>
{
  public void Configure(EntityTypeBuilder<Collection> builder)
  {
  }
}