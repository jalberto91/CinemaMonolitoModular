using FastEndpoints;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CP.Core.Contracts.Core;

public abstract class ValidatedEndpoint<TRequest> : Endpoint<TRequest>
     where TRequest : notnull
{
    public sealed override async Task HandleAsync(TRequest req, CancellationToken ct)
    {
        var validators = HttpContext.RequestServices.GetServices<IValidator<TRequest>>();

        var errors = validators.SelectMany(v => v.Validate(req)).ToList();

        if (errors.Any())
        {
            var dict = errors.GroupBy(e => ToCamel(e.PropertyName)).ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToArray()
                );

            var payload = new
            {
                statusCode = 400,
                message = "Validation failed",
                errors = dict
            };

            HttpContext.Response.StatusCode = 400;
            await HttpContext.Response.WriteAsJsonAsync(payload, ct);
            return;
        }

        await OnValidatedAsync(req, ct);
    }

    protected abstract Task OnValidatedAsync(TRequest req, CancellationToken ct);

    private static string ToCamel(string s)
        => string.IsNullOrEmpty(s) || char.IsLower(s[0])
        ? s : char.ToLowerInvariant(s[0]) + s.Substring(1);
}


