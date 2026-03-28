
using CP.Portal.Movies.Module.Data.Damain;

namespace CP.Portal.Movies.Module.Data.Repositories;

internal interface IMovieRepository : IReadOnlyMoviesRepository
{
    Task AddAsync(Movie movie);
    Task DeleteAsync(Movie movie);
    Task SaveChangesAsync(CancellationToken ct);
}
