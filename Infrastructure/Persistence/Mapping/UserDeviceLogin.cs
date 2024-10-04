using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Infrastructure.Persistence.Models;

namespace Infrastructure.Persistence.Mapping;

public class UserDeviceLoginMapping: IEntityTypeConfiguration<UserDeviceLogin>
{
  public void Configure(EntityTypeBuilder<UserDeviceLogin> builder)
  {
  }
}