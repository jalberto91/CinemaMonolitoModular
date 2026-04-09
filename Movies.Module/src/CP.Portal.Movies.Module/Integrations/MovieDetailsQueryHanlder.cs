using Core.MediatOR.Contracts;
using Core.Results;

using CP.Core.Contracts.MovieDetails;
using CP.Portal.Movies.Module.Services;

namespace CP.Portal.Movies.Module.Integrations;

internal class MovieDetailsQueryHanlder(IMovieService movieService) : IRequestHandler<MovieDetailsQuery, Result<MovieDetailsResponse?>>
{
    private readonly IMovieService _movieService = movieService;

    public async Task<Result<MovieDetailsResponse?>> Handle(MovieDetailsQuery request, CancellationToken cancellationToken)
    {
        var movie = await _movieService.GetMovieByIdAsync(request.MovieId, cancellationToken);

        if(movie is null)
        {
            return Result.NotFound();
        }

        var response = new MovieDetailsResponse(
            movie.MovieId,
            movie.Title,
            movie.Synopsis ?? string.Empty,
            movie.RentalPrice
        );

        return Result.Success<MovieDetailsResponse?>(response);
    }
}
