using CP.Portal.Movies.Module.Data.Damain;

namespace CP.Portal.Movies.Module.Data.Repositories;

internal interface IReadOnlyMoviesRepository
{
    Task<Movie?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<List<Movie>> ListAsync(CancellationToken ct);
}
