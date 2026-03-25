namespace CP.Portal.Movies.Module.Data;

internal class MovieGenre
{
    public Guid MovieId { get; private set; }
    public Guid GenreId { get; private set; }
    public Movie? Movie { get; private set; }
    public Genre? Genre { get; private set; }
}
