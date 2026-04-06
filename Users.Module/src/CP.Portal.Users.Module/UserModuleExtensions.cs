

using CP.Core.Contracts;
using CP.Portal.Users.Module.Data;
using CP.Portal.Users.Module.Data.Domain;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CP.Portal.Users.Module;

public static class UserModuleExtensions
{
    public static IServiceCollection AddUserModuleServices(this IServiceCollection services, ConfigurationManager config)
    {

        services.AddModuleValidators(typeof(UserModuleExtensions).Assembly);

        string? connectionString = config.GetConnectionString("UsersConnectionStrings");
        services.AddDbContext<UserDbContext>(options =>
        {
            options
            .UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention()
            .UseAsyncSeeding( async (db, isFirstRun, ct) => {
                var ctx = (UserDbContext)db;
                if (!await ctx.AppUsers.AnyAsync(ct))
                {
                    var user1 = new AppUser 
                    {
                        UserName = "joaquin.diaz",
                        Email = "jodiaz@mail.com",
                        FullName = "Joaquin Diaz",
                        NormalizedEmail = "JODIAZ@MAIL.COM",
                        NormalizedUserName = "JOAQUIN.DIAZ",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    };

                   var hasher = new PasswordHasher<AppUser>();
                   user1.PasswordHash = hasher.HashPassword(user1, "YourPasswordHere**1");

                    var user2 = new AppUser
                    {
                        UserName = "juan.perez",
                        Email = "juan.perez@mail.com",
                        FullName = "Juan Perez",
                        NormalizedEmail = "JUAN.PEREZ@MAIL.COM",
                        NormalizedUserName = "JUAN.PEREZ",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    };
                    
                    user2.PasswordHash = hasher.HashPassword(user2, "YourPasswordHere**1");

                    await ctx.AppUsers.AddRangeAsync([ user1, user2 ], ct);
                    await ctx.SaveChangesAsync(ct);
                }
            });
        });
        
        
        services.AddIdentityCore<AppUser>()
            .AddEntityFrameworkStores<UserDbContext>();

        return services;
    }
}
