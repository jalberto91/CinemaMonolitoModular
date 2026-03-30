namespace CP.Portal.Movies.Module.Core;

public interface IValidator<TRequest>
{
    IEnumerable<ValidationError> Validate(TRequest request);
}
