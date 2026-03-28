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

        SeedGenres(modelBuilder);
        SeedPeoples(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void SeedGenres(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new Genre (Guid.Parse("00000000-0000-0000-0000-000000000001"), "Action"),
            new Genre (Guid.Parse("00000000-0000-0000-0000-000000000002"), "Drama"),
            new Genre (Guid.Parse("00000000-0000-0000-0000-000000000003"), "Comedy"),
            new Genre (Guid.Parse("00000000-0000-0000-0000-000000000004"), "Sci-Fi"),
            new Genre (Guid.Parse("00000000-0000-0000-0000-000000000005"), "Thriller"),
            new Genre (Guid.Parse("00000000-0000-0000-0000-000000000006"), "Fantasy"),
            new Genre (Guid.Parse("00000000-0000-0000-0000-000000000007"), "Horror")
        );
    }

    private static void SeedPeoples(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().HasData(
            new Person (Guid.Parse("00000000-0000-0000-0000-000000000001"), "John Doe", Utc(1980, 1, 1), "Bio for John Doe"),
            new Person (Guid.Parse("00000000-0000-0000-0000-000000000002"), "Jane Smith", Utc(1985, 2, 2), "Bio for Jane Smith"),
            new Person (Guid.Parse("00000000-0000-0000-0000-000000000003"), "Michael Johnson", Utc(1990, 3, 3), "Bio for Michael Johnson"),
            new Person (Guid.Parse("00000000-0000-0000-0000-000000000004"), "Emily Davis", Utc(1995, 4, 4), null),
            new Person (Guid.Parse("00000000-0000-0000-0000-000000000005"), "David Brown", Utc(2000, 5, 5), "Bio for David Brown"),
            new Person (Guid.Parse("00000000-0000-0000-0000-000000000006"), "Sarah Wilson", Utc(1988, 6, 6), "Bio for Sarah Wilson"),
            new Person (Guid.Parse("00000000-0000-0000-0000-000000000007"), "Chris Lee", Utc(1992, 7, 7), "Bio for Chris Lee")
        );
    }

    private static DateTime Utc(int y, int m, int d) => new DateTime(y, m, d, 0, 0, 0, DateTimeKind.Utc);

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>()
            .HavePrecision(18, 6);
    }

}
