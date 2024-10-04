using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Infrastructure.Persistence.Models;

namespace Infrastructure.Persistence.Mapping;

public class AnswerMapping: IEntityTypeConfiguration<Answer>
{
  public void Configure(EntityTypeBuilder<Answer> builder)
  {
  }
}