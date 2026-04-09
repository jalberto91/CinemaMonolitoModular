using CP.Core.Contracts.Core;
using CP.Portal.Users.Module.Data.Domain;

using Microsoft.AspNetCore.Identity;

namespace CP.Portal.Users.Module.Endpoints.CreateUser;

public sealed record CreateUserRequest(string FullName, string Email, string password);

public sealed class CreateUserValidator : IValidator<CreateUserRequest>
{
    public IEnumerable<ValidationError> Validate(CreateUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
        {
            yield return new ValidationError(nameof(request.Email), "Email is required");
        }

        if (string.IsNullOrWhiteSpace(request.password))
        {
            yield return new ValidationError(nameof(request.password), "Password is required");
        }
    }
}

internal class CreateUserEndpoint(UserManager<AppUser> userManager) : ValidatedEndpoint<CreateUserRequest>
{
    private readonly UserManager<AppUser> _userManager = userManager;

    public override void Configure()
    {
        Post("/api/users");
        AllowAnonymous();
    }   

    protected override async Task OnValidatedAsync(CreateUserRequest req, CancellationToken ct)
    {
        var newUser = new AppUser
        {
            Email = req.Email,
            UserName = req.Email,
            FullName = req.FullName
        };

        await _userManager.CreateAsync(newUser, req.password);
        await Send.OkAsync();
    }
}
