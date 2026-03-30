public class CreateMovieRequest
{
    public string Title { get; set; } = string.Empty;
    public DateOnly ReleaseYear { get; set; }
    public int DurationMinutes { get; set; }
    public string? Language { get; set; }
    public decimal Price { get; set; }
    public string? OriginalTitle { get; set; }
    public string? Description { get; set; }
}
