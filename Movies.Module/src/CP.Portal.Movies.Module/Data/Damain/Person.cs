namespace CP.Portal.Movies.Module.Data.Damain;

internal class Person
{
    public Guid PersonId { get; private set; } = Guid.CreateVersion7();
    public string? Name{ get; private set; }
    public DateTime BirthDate { get; private set; }
    public string? Bio { get; private set; }

    public ICollection<MovieCast> Casts { get; } = [];
    public ICollection<MovieCrew> Crewers { get; } = [];

    internal Person(Guid personId, string name, DateTime birthDate, string? bio)
    {
        PersonId = personId;
        Name = name;
        BirthDate = birthDate;
        Bio = bio;
    }
}
