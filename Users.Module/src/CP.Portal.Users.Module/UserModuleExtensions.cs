

using CP.Core.Contracts;

using Microsoft.Extensions.DependencyInjection;

namespace CP.Portal.Users.Module;

public static class UserModuleExtensions
{
    public static IServiceCollection AddUserModuleServices(this IServiceCollection services)
    {

        services.AddModuleValidators(typeof(UserModuleExtensions).Assembly);

        return services;
    }
}
