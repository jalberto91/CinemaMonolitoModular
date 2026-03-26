using CP.Portal.Movies.Module.Data.Damain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CP.Portal.Movies.Module.Data.Configurations;

internal class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("movies", "movies");
        builder.HasKey(p => p.MovieId);
        builder.Property(p => p.MovieId).ValueGeneratedNever();
        builder.Property(p => p.Title).HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
        builder.Property(p => p.OriginalTitle).HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
        builder.Property(p => p.Synopsis).HasMaxLength(400);
        builder.Property(p => p.Language).IsRequired();

        builder.HasMany(p => p.MovieGenres)
            .WithOne(p => p.Movie)
            .HasForeignKey(p => p.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Casts)
            .WithOne(p => p.Movie)
            .HasForeignKey(p => p.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Crewers)
            .WithOne(p => p.Movie)
            .HasForeignKey(p => p.MovieId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
