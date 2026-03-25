namespace CP.Portal.Movies.Module.Data;

internal class Genre
{
    public Guid GenreId { get; private set; } = Guid.CreateVersion7();
    public string? Name { get; private set; }

}
