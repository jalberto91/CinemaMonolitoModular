using CP.Portal.Movies.Module.Services;

using FastEndpoints;

namespace CP.Portal.Movies.Module.Endpoints;

internal class ListMoviesEndpoint(IMovieService movieService)
    : EndpointWithoutRequest<ListMoviesResponse>
{
    private readonly IMovieService _movieService =  movieService;

    public override void Configure()
    {
        Get("api/movies");
        AllowAnonymous();
    }

    public override Task HandleAsync(CancellationToken ct = default)
    {
        var movies = _movieService.GetMovies();
        return Send.OkAsync(new ListMoviesResponse { Movies = movies }, ct);
    }
}