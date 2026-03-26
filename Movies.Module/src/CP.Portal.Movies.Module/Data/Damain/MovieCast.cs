namespace CP.Portal.Movies.Module.Data.Damain;

internal class MovieCast
{
    public Guid MovieId { get; private set; }
    public Guid PersonId { get; private set; }
    public string? CharacterName { get; private set; }
    public int CastOrder { get; private set; }

    public Movie? Movie { get; private set; }
    public Person? Person { get; private set; }
}
