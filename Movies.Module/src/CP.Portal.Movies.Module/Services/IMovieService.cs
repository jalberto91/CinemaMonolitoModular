using CP.Portal.Movies.Module.Data.Damain;
using CP.Portal.Movies.Module.Endpoints;

namespace CP.Portal.Movies.Module.Services;

internal interface IMovieService
{
    Task<List<Movie>> ListMovieAsync(CancellationToken ct);
    Task<Movie?> GetMovieByIdAsync(Guid id, CancellationToken ct);
    Task CreateMovieAsync(Movie newMovie, CancellationToken ct);
    Task DeleteMovieAsync(Guid id, CancellationToken ct);
    Task UpdateMoviePriceAsync(Guid id, decimal newPrice, CancellationToken ct);
}
