using System.Security.Claims;

using CP.Portal.Users.Module.Data.Domain;

using FastEndpoints;

using Microsoft.AspNetCore.Identity;

namespace CP.Portal.Users.Module.Endpoints.GetUser;

public sealed record UserResponse(string Id, string FullName, string Email, string Username);

internal class GetUserEndpoint(UserManager<AppUser> userManager) : EndpointWithoutRequest<UserResponse>
{
    private readonly UserManager<AppUser> _userManager = userManager;

    public override void Configure()
    {
        Get("api/users/me");
        AuthSchemes("Bearer");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userId))
        {
            await Send.UnauthorizedAsync(ct);
            return;
        }

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            await Send.UnauthorizedAsync(ct);
            return;
        }

        var response = new UserResponse(user.Id, user.FullName, user.Email!, user.UserName!);

        await Send.OkAsync(response, ct);
    }

}
