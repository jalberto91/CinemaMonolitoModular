
using CP.Portal.Movies.Module.Data.Damain;

using Microsoft.EntityFrameworkCore;

namespace CP.Portal.Movies.Module.Data.Seedings;

internal static class MovieGenresAsyncSeeder
{
    public static async Task SeedAsync(
        MovieDbContext db,
        IReadOnlyDictionary<string, Guid> movies,
        CancellationToken ct
    )
    {
        if (await db.MovieGenres.AnyAsync(ct))
            return;

        var shawshankRedemptionId = movies["The Shawshank Redemption"];
        var GodfatherId = movies["The Godfather"];
        var darkKnightId = movies["The Dark Knight"];

        await db.MovieGenres.AddRangeAsync  (
            [
                new MovieGenre ( shawshankRedemptionId, SeedConstants.GENRE_DRAMA ),

                new MovieGenre ( GodfatherId, SeedConstants.GENRE_DRAMA ),
                new MovieGenre ( GodfatherId, SeedConstants.GENRE_THRILLER ),

                new MovieGenre ( darkKnightId, SeedConstants.GENRE_ACTION ),
                new MovieGenre ( darkKnightId, SeedConstants.GENRE_FANTASY ),
            ], ct
        );

        await db.SaveChangesAsync(ct);

    }
}
