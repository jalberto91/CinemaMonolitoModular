using CP.Portal.Movies.Module.Core;
using CP.Portal.Movies.Module.Endpoints;
using CP.Portal.Movies.Module.Services;

internal class CreateMovieEndpoint(IMovieService movieService) : ValidatedEndpoint<CreateMovieRequest>
{
    private readonly IMovieService _movieService = movieService;

    public override void Configure()
    {
        Post("api/movies");
        AllowAnonymous();
    }
    protected override async Task OnValidatedAsync(CreateMovieRequest req, CancellationToken ct)
    {
        var movie = req.ToMovie();
        await _movieService.CreateMovieAsync(movie, ct);
        await Send.OkAsync(ct);
    }
}
