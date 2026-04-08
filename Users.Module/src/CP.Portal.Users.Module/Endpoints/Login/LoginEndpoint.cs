using CP.Portal.Users.Module.Data.Domain;

using FastEndpoints;

using Microsoft.AspNetCore.Identity;

namespace CP.Portal.Users.Module.Endpoints.Login;

public sealed record LoginRequest(string Email, string Password);

internal class LoginEndpoint(UserManager<AppUser> userManager) : Endpoint<LoginRequest>
{
    private readonly UserManager<AppUser> _userManager = userManager;

    public override void Configure()
    {
        Post("/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var user = await _userManager.FindByEmailAsync(req.Email);
        if (user is null)
        {
            await Send.UnauthorizedAsync();
            return;
        }

        var loginSuccesful = await _userManager.CheckPasswordAsync(user, req.Password);
        if (!loginSuccesful)
        {
            await Send.UnauthorizedAsync();
            return;
        }

        await Send.OkAsync();
    }
}