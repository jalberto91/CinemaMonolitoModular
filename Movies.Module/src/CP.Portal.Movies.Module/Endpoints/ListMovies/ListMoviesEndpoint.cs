using CP.Portal.Movies.Module.Data.Damain;
using CP.Portal.Movies.Module.Services;

using FastEndpoints;

namespace CP.Portal.Movies.Module.Endpoints.ListMovies;

internal class ListMoviesEndpoint(IMovieService movieService)
    : EndpointWithoutRequest<ListMoviesResponse>
{
    private readonly IMovieService _movieService =  movieService;

    public override void Configure()
    {
        Get("api/movies");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct = default)
    {
        var movies = await _movieService.ListMovieAsync(ct);
        var moviesResponse = movies.Select(Movie => Movie.ToMovieResponse()).ToList();

        var response = new ListMoviesResponse
        {
            Movies = moviesResponse
        };

        await Send.OkAsync(response, ct);
    }
}