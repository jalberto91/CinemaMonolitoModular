using Core.MediatOR.Contracts;
using Core.Results;

using CP.Core.Contracts.MovieDetails;
using CP.Portal.Users.Module.Data.Domain;
using CP.Portal.Users.Module.Data.Repositories;

namespace CP.Portal.Users.Module.UseCases;

public record AddMovieToCardCommand(Guid MovieId, int Quantity, string EmailAddress) : IRequest<Result>;

internal class AddMovieToCardHandler(
    IAppUserRepository appUserRepository,
    IMediatOR mediatoR
    ) : IRequestHandler<AddMovieToCardCommand, Result>
{
    private readonly IAppUserRepository _appUserRepository = appUserRepository;
    private readonly IMediatOR _mediatoR = mediatoR;

    public async Task<Result> Handle(AddMovieToCardCommand request, CancellationToken cancellationToken)
    {
        var user = await _appUserRepository.GetUserWithCartByEmailAsync(request.EmailAddress, cancellationToken);

        if (user is null)
        {
            return Result.Unauthorized();
        }

        var query = new MovieDetailsQuery(request.MovieId);
        var result = await _mediatoR.Send(query, cancellationToken);

        if (result.Status == ResultStatus.NotFound)
        {
            return Result.NotFound();
        }

        var movieDetails = result.Value;

        var newCardItem = new CartMovie(
            movieDetails!.MovieId,
            movieDetails.Title,
            request.Quantity,
            movieDetails.Price
        );

        user.AddMovieToCard(newCardItem);
        await _appUserRepository.SaveChangeAsync(cancellationToken);

        return Result.Success();
    }

}
