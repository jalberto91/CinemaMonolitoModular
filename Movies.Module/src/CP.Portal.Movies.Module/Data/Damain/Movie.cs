using System.ComponentModel.DataAnnotations.Schema;

namespace CP.Portal.Movies.Module.Data.Damain;

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

    public ICollection<MovieCast> Casts { get; } = [];
    public ICollection<MovieCrew> Crewers { get; } = [];
    public ICollection<MovieGenre> MovieGenres { get; } = [];

    //projections
    [NotMapped]
    public IEnumerable<Genre> Genres => MovieGenres
                                        .Select(g => g.Genre!)
                                        .Where(g => g != null);

    [NotMapped]
    public IEnumerable<Person> CastPeple => Casts
                                            .Select(c => c.Person!)
                                            .Where(c=> c != null);


    [NotMapped]
    public IEnumerable<Person> CrewPeople => Crewers
                                            .Select(c => c.Person!)
                                            .Where(c => c != null);




    internal Movie(
        string title,
        DateOnly releaseYear,
        int durationMinutes,
        string language,
        decimal rentalPrice,
        string? synopsis = null,
        string? originalTitle = null
        )
    {
        if(string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be null or empty.", nameof(title));
        }

        if (synopsis is not null && synopsis.Length > 4000)
        {
            throw new ArgumentException("Synopsis cannot exceed 4000 characters.", nameof(synopsis));
        }

        if (rentalPrice < 0m)
        { 
            throw new ArgumentOutOfRangeException(nameof(rentalPrice), "Rental price cannot be negative.");
        }

        Title = title.Trim();
        OriginalTitle = originalTitle?.Trim();
        Synopsis = synopsis?.Trim();
        ReleaseYear = releaseYear;
        DurationMinutes = durationMinutes;
        Language = language.Trim();
        RentalPrice = rentalPrice;
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0m)
        {
            throw new ArgumentOutOfRangeException(nameof(newPrice), "Rental price cannot be negative.");
        }

        if (newPrice == RentalPrice)
        { 
          return;
        }
        

        RentalPrice = newPrice;
    }
}
