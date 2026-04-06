using CP.Portal.Users.Module.Data.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CP.Portal.Users.Module.Data.Configurations;

public sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
       builder.Navigation(u => u.CartItems)
            .HasField("_cartMovies")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(u => u.CartItems)
               .WithOne(ci => ci.User)
               .HasForeignKey(ci => ci.UserId)
               .IsRequired();
    }
}
