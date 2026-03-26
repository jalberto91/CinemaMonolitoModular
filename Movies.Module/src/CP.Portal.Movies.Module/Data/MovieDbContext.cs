using System.Reflection;

using CP.Portal.Movies.Module.Data.Damain;

using Microsoft.EntityFrameworkCore;

namespace CP.Portal.Movies.Module.Data;

public class MovieDbContext: DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options)
        : base(options)
    {}

    internal DbSet<Movie> Movies { get; set; }
    internal DbSet<Genre> Genres { get; set; }
    internal DbSet<Person> Peoples { get; set; }
    internal DbSet<MovieGenre> MovieGenres { get; set; }
    internal DbSet<MovieCast> MovieCasts { get; set; }
    internal DbSet<MovieCrew> GetMovieCrews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("movies");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>()
            .HavePrecision(18, 6);
    }

}
