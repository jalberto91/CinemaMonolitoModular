namespace CP.Portal.Movies.Module.Data.Damain;

internal class MovieCrew
{
    public Guid MovieId { get; private set; }
    public Guid PersonId { get; private set; }
    public string? Role { get; private set; }

    public Movie? Movie { get; private set; }
    public Person? Person { get; private set; }
}
