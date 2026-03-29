using CP.Portal.Movies.Module.Data.Damain;
using CP.Portal.Movies.Module.Data.Repositories;

namespace CP.Portal.Movies.Module.Services;

internal class MovieService(IMovieRepository movieRepository) : IMovieService
{
    private readonly IMovieRepository _movieRepository = movieRepository;

    public async Task CreateMovieAsync(Movie newMovie, CancellationToken ct)
    { 
        await _movieRepository.AddAsync(newMovie);
        await _movieRepository.SaveChangesAsync(ct);
    }
    public async Task DeleteMovieAsync(Guid id, CancellationToken ct)
    {
        var movieToDelete = await _movieRepository.GetByIdAsync(id, ct);
        if (movieToDelete is not null)
        {
            await _movieRepository.DeleteAsync(movieToDelete);
            await _movieRepository.SaveChangesAsync(ct);
        }
    }
    public Task<Movie?> GetMovieByIdAsync(Guid id, CancellationToken ct)
    {
        return _movieRepository.GetByIdAsync(id, ct);
    }
    public Task<List<Movie>> ListMovieAsync(CancellationToken ct)
    {
        return _movieRepository.ListAsync(ct);
    }
    public async Task UpdateMoviePriceAsync(Guid id, decimal newPrice, CancellationToken ct)
    {
        var movieToUpdate = await _movieRepository.GetByIdAsync(id, ct);
        if(movieToUpdate is not null)
        {
            movieToUpdate.UpdatePrice(newPrice);
            await _movieRepository.SaveChangesAsync(ct);
        }
    }
}
