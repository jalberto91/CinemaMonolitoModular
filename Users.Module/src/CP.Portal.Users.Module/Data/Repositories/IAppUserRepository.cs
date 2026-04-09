using CP.Portal.Users.Module.Data.Domain;

namespace CP.Portal.Users.Module.Data.Repositories;

public interface IAppUserRepository
{
    public Task<AppUser?> GetUserWithCartByEmailAsync(string email, CancellationToken ct); 
    public Task SaveChangeAsync(CancellationToken ct);
}
