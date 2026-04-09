
using CP.Portal.Users.Module.Data.Domain;

using Microsoft.EntityFrameworkCore;

namespace CP.Portal.Users.Module.Data.Repositories;

internal class EFAppUserRepository(UserDbContext userDbContext) : IAppUserRepository
{
    private readonly UserDbContext _userDbContext = userDbContext;

    public async Task<AppUser?> GetUserWithCartByEmailAsync(string email, CancellationToken ct)
    {
      return await _userDbContext.AppUsers.Include(u => u.CartItems).FirstOrDefaultAsync(u => u.Email == email, ct);
    }
    public Task SaveChangeAsync(CancellationToken ct)
    {
        return _userDbContext.SaveChangesAsync(ct);
    }
}
