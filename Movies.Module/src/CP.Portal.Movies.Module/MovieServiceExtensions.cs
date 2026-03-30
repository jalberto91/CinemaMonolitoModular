using CP.Portal.Movies.Module.Core;
using CP.Portal.Movies.Module.Data;
using CP.Portal.Movies.Module.Data.Repositories;
using CP.Portal.Movies.Module.Data.Seedings;
using CP.Portal.Movies.Module.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CP.Portal.Movies.Module;

public static class MovieServiceExtensions
{
    public static IServiceCollection AddMovieServices(
        this IServiceCollection services,
        ConfigurationManager config
        )
    {
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<IMovieRepository, EfMovieRepository>();

        string? connectionString = config.GetConnectionString("MoviesConnectionStrings");
        services.AddDbContext<MovieDbContext>(options =>
        {
            options.UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention()
            .UseAsyncSeeding( async (db, isFirst, ct) =>
            {
                var dbContext = (MovieDbContext)db;

                var movies = await MoviesAsyncSeeder.SeedAsync(dbContext, ct);

                await MovieGenresAsyncSeeder.SeedAsync(dbContext, movies, ct);
                await MovieCastsAsyncSeeder.SeedAsync(dbContext, movies, ct);
                await MoviesCrewsAsyncSeeder.SeedAsync(dbContext, movies, ct);   

            });
        });

        var assembly = typeof(MovieServiceExtensions).Assembly;

        var validatorTypes = assembly.GetTypes().Where(
            t => t.IsClass && !t.IsAbstract
            && t.GetInterfaces()
            .Any(
                i => i.IsGenericType
                && i.GetGenericTypeDefinition() == typeof(IValidator<>))
        ).ToList();

        foreach(var validatorType in validatorTypes)
        {
            var validatorInterface = validatorType.GetInterfaces()
                            .First(i => i.IsGenericType
                            && i.GetGenericTypeDefinition() == typeof(IValidator<>));

            services.AddScoped(validatorInterface, validatorType);
        }

        return services;
    }
}
