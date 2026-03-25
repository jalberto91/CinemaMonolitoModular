using CP.Portal.Movies.Module.Endpoints;

namespace CP.Portal.Movies.Module.Services;

internal interface IMovieService
{
    List<MovieResponse> GetMovies();
}
