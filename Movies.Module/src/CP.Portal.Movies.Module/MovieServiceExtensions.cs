using Core.MediatOR;

using CP.Core.Contracts;
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

        services.AddModuleValidators(typeof(MovieServiceExtensions).Assembly);
        services.AddMediatOR(typeof(MovieServiceExtensions).Assembly);

        return services;
    }
}
