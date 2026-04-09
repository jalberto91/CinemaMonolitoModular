using System.Security.Claims;

using Core.MediatOR.Contracts;
using Core.Results;

using CP.Portal.Users.Module.UseCases;

using FastEndpoints;

namespace CP.Portal.Users.Module.Endpoints.AddMovie;

public record AddCartMovieRequest(Guid MovieId, int Quantity);

internal class AddMovieEndpoint(IMediatOR mediatOR) : Endpoint<AddCartMovieRequest>
{
    private readonly IMediatOR _mediator = mediatOR;

    public override void Configure()
    {
        Post("/api/cart");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(AddCartMovieRequest req, CancellationToken ct)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");

        var command = new AddMovieToCardCommand(
                req.MovieId,
                req.Quantity,
                emailAddress!
        );

        var result = await _mediator.Send(command, ct);

        if(result.Status == ResultStatus.Unauthorized)
        {
            await Send.UnauthorizedAsync();
        }

        await Send.OkAsync();
    }

}
