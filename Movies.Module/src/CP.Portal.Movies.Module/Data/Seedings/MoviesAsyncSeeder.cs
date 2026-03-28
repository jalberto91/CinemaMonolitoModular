using CP.Portal.Movies.Module.Data.Damain;

using Microsoft.EntityFrameworkCore;

namespace CP.Portal.Movies.Module.Data.Seedings;

internal static class MoviesAsyncSeeder
{
    public static async Task<Dictionary<string, Guid>> SeedAsync(
        MovieDbContext db,
        CancellationToken ct
    )
    { 
        var map = new Dictionary<string, Guid>(StringComparer.OrdinalIgnoreCase);

        if (await db.Movies.AnyAsync(ct))
        {
            var moviesFromDb = await db.Movies
                .Select(m => new { m.Title, m.MovieId })
                .ToListAsync(ct);

            foreach (var movie in moviesFromDb)
                map[movie.Title] = movie.MovieId;

            return map;
        }

        var m1 = new Movie(
                "The Shawshank Redemption",
                new DateOnly(1994, 9, 22),
                136,
                "en",
                12.34m,
                "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency."
            );

        var m2 = new Movie(
                "The Godfather",
                new DateOnly(1972, 3, 24),
                175,
                "en",
                15.99m,
                "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son."
            );

        var m3 = new Movie(
                "The Dark Knight",
                new DateOnly(2008, 7, 18),
                152,
                "en",
                14.99m,
                "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham."
            );

        await db.Movies.AddRangeAsync([ m1, m2, m3 ], ct);
        await db.SaveChangesAsync(ct);

        map[m1.Title] = m1.MovieId;
        map[m2.Title] = m2.MovieId;
        map[m3.Title] = m3.MovieId;

        return map;
    }
}
