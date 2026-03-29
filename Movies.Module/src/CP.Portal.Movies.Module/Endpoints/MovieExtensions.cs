using CP.Portal.Movies.Module.Data.Damain;

namespace CP.Portal.Movies.Module.Endpoints;

internal static class MovieExtensions
{
    extension(Movie movie)
    {
        public MovieResponse ToMovieResponse()
        {
            return new MovieResponse(
                movie.MovieId, 
                movie.Title, 
                movie.Synopsis ?? string.Empty
            );
        }
    }
}