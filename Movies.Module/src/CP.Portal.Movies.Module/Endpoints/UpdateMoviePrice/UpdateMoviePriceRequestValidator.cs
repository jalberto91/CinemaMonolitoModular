using CP.Portal.Movies.Module.Core;

namespace CP.Portal.Movies.Module.Endpoints.UpdateMoviePrice;

internal sealed class UpdateMoviePriceRequestValidator : IValidator<UpdateMoviePriceRequest>
{
    public IEnumerable<ValidationError> Validate(UpdateMoviePriceRequest request)
    {
        if (request.NewPrice <= 0)
        {
            yield return new ValidationError(
                nameof(request.NewPrice),
                "Price must be greater than zero"
            );
        }
    }
}
