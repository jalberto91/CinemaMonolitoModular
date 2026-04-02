using CP.Portal.Users.Module.Data.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CP.Portal.Users.Module.Data.Configurations;

public sealed class CardMovieConfiguration : IEntityTypeConfiguration<CartMovie>
{
    public void Configure(EntityTypeBuilder<CartMovie> builder)
    { 
        builder.ToTable("cart_movies", "users");
        builder.HasKey(cm => new { cm.UserId, cm.MovieId });

        builder.HasOne(cm => cm.User)
            .WithMany(u => u.CartItems)
            .HasForeignKey(cm => cm.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
