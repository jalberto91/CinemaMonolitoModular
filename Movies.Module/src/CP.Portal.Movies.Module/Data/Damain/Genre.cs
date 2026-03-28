namespace CP.Portal.Movies.Module.Data.Damain;

internal class Genre
{
    public Guid GenreId { get; private set; } = Guid.CreateVersion7();
    public string? Name { get; private set; }

    public ICollection<MovieGenre> MovieGenres { get; } = [];

    internal Genre(Guid genreId, string name)
    {
        GenreId = genreId;
        Name = name;
    }
}
