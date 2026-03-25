using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CP.Portal.Movies.Module;

public record MovieResponse(Guid Id, string Title, string Description);

public interface IMovieService
{
    List<MovieResponse> GetMovies();
}

public class MovieService : IMovieService
{
    public List<MovieResponse> GetMovies() =>
        [
            new MovieResponse(Guid.NewGuid(), "The Shawshank Redemption", "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency."),
            new MovieResponse(Guid.NewGuid(), "The Godfather", "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son."),
            new MovieResponse(Guid.NewGuid(), "The Dark Knight", "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham. The Dark Knight must accept one of the greatest psychological and physical tests of his ability to fight injustice.")
        ];
}

public static class MovieServiceExtensions
{
    public static IServiceCollection AddMovieServices(this IServiceCollection services)
    {
        services.AddScoped<IMovieService, MovieService>();
        return services;
    }
}

public static class MovieEndpoints
{
    public static void MapMoviesEndpoints(this WebApplication app) 
    {
        app.MapGet("/movies", (IMovieService movieService) =>
        {
            return movieService.GetMovies();
        });
    }
}