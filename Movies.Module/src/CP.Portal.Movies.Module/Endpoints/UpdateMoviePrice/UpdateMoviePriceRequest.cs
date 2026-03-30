namespace CP.Portal.Movies.Module.Endpoints.UpdateMoviePrice;

public class UpdateMoviePriceRequest
{
    public Guid Id { get; set; }
    public decimal NewPrice { get; set; }
}
