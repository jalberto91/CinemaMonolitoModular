using CP.Portal.Users.Module.Data.Domain;

using FastEndpoints;
using FastEndpoints.Security;

using Microsoft.AspNetCore.Identity;

namespace CP.Portal.Users.Module.Endpoints.Login;

public sealed record LoginRequest(string Email, string Password);

internal class LoginEndpoint(UserManager<AppUser> userManager) : Endpoint<LoginRequest>
{
    private readonly UserManager<AppUser> _userManager = userManager;

    public override void Configure()
    {
        Post("api/users/login");
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

        var jwtSecret = Config["Auth:JwtSecret"]!;

        var token = JwtBearer.CreateToken(option => 
        { 
            option.SigningKey = jwtSecret; 
            option.ExpireAt = DateTime.UtcNow.AddHours(500);
            option.User["sub"] = user.Id;
            option.User["email"] = user.Email!;
            option.User["name"] = user.FullName!;
            option.User["EmailAddress"] = user.Email!;
        });

        await Send.OkAsync(token);
    }
}