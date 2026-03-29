
using CP.Portal.Movies.Module.Data.Damain;

using Microsoft.EntityFrameworkCore;

namespace CP.Portal.Movies.Module.Data.Repositories;

internal class EfMovieRepository(MovieDbContext dbContext) : IMovieRepository
{
    private readonly MovieDbContext _dbContext = dbContext;

    public Task AddAsync(Movie movie)
    {
        _dbContext.Add(movie);
        return Task.CompletedTask;
    }
    public Task DeleteAsync(Movie movie)
    {
        _dbContext.Remove(movie);
        return Task.CompletedTask;
    }
    public async Task<Movie?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return  await _dbContext.Movies.FindAsync(id, ct);
    }
    public async Task<List<Movie>> ListAsync(CancellationToken ct)
    {
        return await _dbContext.Movies.ToListAsync(ct);
    }
    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await _dbContext.SaveChangesAsync(ct);
    }
}
