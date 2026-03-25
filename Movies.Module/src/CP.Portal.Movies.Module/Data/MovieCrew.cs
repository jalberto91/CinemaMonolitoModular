
namespace CP.Portal.Movies.Module.Data;

internal class MovieCrew
{
    public Guid MovieId { get; private set; }
    public Guid PersonId { get; private set; }
    public string? Role { get; private set; }
}
