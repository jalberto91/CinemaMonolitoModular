using CP.Portal.Movies.Module.Data.Damain;

using Microsoft.EntityFrameworkCore;


namespace CP.Portal.Movies.Module.Data.Seedings;

internal  static class MovieCastsAsyncSeeder
{
    public static async Task SeedAsync(
        MovieDbContext db,
        IReadOnlyDictionary<string, Guid> movies,
        CancellationToken ct
    )
    {
        if (await db.MovieCasts.AnyAsync(ct))
            return;

        var shawshankRedemptionId = movies["The Shawshank Redemption"];
        var godfatherId = movies["The Godfather"];
        var darkKnightId = movies["The Dark Knight"];

        await db.MovieCasts.AddRangeAsync(
          [
            new MovieCast(shawshankRedemptionId, SeedConstants.PERSON_CHRIS_LEE, "Ellis Boyd 'Red' Redding", 1),
            new MovieCast(godfatherId, SeedConstants.PERSON_EMILY_DAVIS, "Michael Corleone", 2),
            new MovieCast(darkKnightId, SeedConstants.PERSON_JOHN_DOE, "Bruce Wayne", 1)
          ], ct
        );

        await db.SaveChangesAsync(ct);
    }
}
