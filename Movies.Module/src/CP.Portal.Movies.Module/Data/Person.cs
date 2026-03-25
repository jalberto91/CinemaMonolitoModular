namespace CP.Portal.Movies.Module.Data;

internal class Person
{
    public Guid PersonId { get; private set; } = Guid.CreateVersion7();
    public string? Name{ get; private set; }
    public DateTime BirthDate { get; private set; }
    public string? Bio { get; private set; }
}
