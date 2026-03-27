using CP.Portal.Movies.Module.Data.Damain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CP.Portal.Movies.Module.Data.Configurations;

internal class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("genres", "movies");
        builder.HasKey(p => p.GenreId);
        builder.Property(p => p.GenreId).ValueGeneratedNever();

        builder.Property(p => p.Name)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .IsRequired();

        builder.HasMany(p => p.MovieGenres)
            .WithOne(p => p.Genre)
            .HasForeignKey(p => p.GenreId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
