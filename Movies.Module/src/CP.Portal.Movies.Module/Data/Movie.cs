namespace CP.Portal.Movies.Module.Data;

internal class Movie
{
    public Guid MovieId { get; private set; } = Guid.CreateVersion7();
    public string Title { get; private set; } = string.Empty;
    public string? OriginalTitle { get; private set; }
    public  string? Synopsis { get; private set; }
    public DateOnly ReleaseYear { get; private set; }
    public int DurationMinutes { get; private set; }
    public string? Language { get; private set; }
    public decimal RentalPrice { get; private set; }
    public DateTime CreateAt { get; private set; } = DateTime.UtcNow;
}
