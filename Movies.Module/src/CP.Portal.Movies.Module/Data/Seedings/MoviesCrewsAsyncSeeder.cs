
using CP.Portal.Movies.Module.Data.Damain;

using Microsoft.EntityFrameworkCore;

namespace CP.Portal.Movies.Module.Data.Seedings;

internal static class MoviesCrewsAsyncSeeder
{
    public static async Task SeedAsync(
        MovieDbContext db,
        IReadOnlyDictionary<string, Guid> movies,
        CancellationToken ct
    )
    {
        if (await db.MovieCrews.AnyAsync(ct))
            return;

        var shawshankRedemptionId = movies["The Shawshank Redemption"];
        var godfatherId = movies["The Godfather"];
        var darkKnightId = movies["The Dark Knight"];

        await db.MovieCrews.AddRangeAsync(
          [
            new MovieCrew(shawshankRedemptionId, SeedConstants.PERSON_JANE_SMITH, "Director"),
            new MovieCrew(godfatherId, SeedConstants.PERSON_DAVID_BROWN, "Director"),
            new MovieCrew(darkKnightId, SeedConstants.PERSON_DAVID_BROWN, "Director"),
          ], ct
        );

        await db.SaveChangesAsync(ct);
    }
}
