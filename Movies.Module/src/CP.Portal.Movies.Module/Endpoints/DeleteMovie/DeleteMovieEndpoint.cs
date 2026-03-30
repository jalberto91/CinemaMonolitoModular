
using CP.Portal.Movies.Module.Services;

using FastEndpoints;

namespace CP.Portal.Movies.Module.Endpoints.DeleteMovie;

internal class DeleteMovieEndpoint(IMovieService movieService) : Endpoint<DeleteMovieRequest>
{
    private readonly IMovieService _movieService = movieService;

    public override void Configure()
    {
        Delete("api/movies/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteMovieRequest req, CancellationToken ct)
    {
        await _movieService.DeleteMovieAsync(req.Id, ct);
        await Send.NoContentAsync(ct);
    }
}


public class DeleteMovieRequest
{
    public Guid Id { get; set; }
}