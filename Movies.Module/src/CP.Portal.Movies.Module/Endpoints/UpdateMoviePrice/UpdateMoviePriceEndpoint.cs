using CP.Core.Contracts.Core;
using CP.Portal.Movies.Module.Services;

namespace CP.Portal.Movies.Module.Endpoints.UpdateMoviePrice;

internal class UpdateMoviePriceEndpoint(IMovieService movieService) : ValidatedEndpoint<UpdateMoviePriceRequest>
{
    private readonly IMovieService _movieService = movieService;

    public override void Configure()
    {
        Put("api/movies/{Id}/price");
        AllowAnonymous();
    }

    protected override async Task OnValidatedAsync(UpdateMoviePriceRequest req, CancellationToken ct)
    {
        await _movieService.UpdateMoviePriceAsync(req.Id, req.NewPrice, ct);
        await Send.NoContentAsync();
    }
}