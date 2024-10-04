using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Infrastructure.Persistence.Models;

namespace Infrastructure.Persistence.Mapping;

public class QuestionMapping: IEntityTypeConfiguration<Question>
{
  public void Configure(EntityTypeBuilder<Question> builder)
  {
  }
}