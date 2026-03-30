using CP.Portal.Movies.Module.Core;

internal sealed class CreateMovieRequestValidator : IValidator<CreateMovieRequest>
{
    public IEnumerable<ValidationError> Validate(CreateMovieRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        { 
            yield return new ValidationError(nameof(request.Title), "Title is required");
        }

        if(string.IsNullOrEmpty(request.Description))
        {
            yield return new ValidationError(nameof(request.Description), "Description is required");
        }

        if (request.DurationMinutes <= 0)
        {
            yield return new ValidationError(nameof(request.DurationMinutes), "Duration must be greater than 0");
        }

        if (request.Price <= 0m)
        {
            yield return new ValidationError(nameof(request.Price), "Price must be greater than 0");
        }
    }
}