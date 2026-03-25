using Microsoft.AspNetCore.Builder;

namespace CP.Portal.Movies.Module;

public static class MovieEndpoints
{
    public static void MapMoviesEndpoints(this WebApplication app) 
    {
        app.MapGet("/test", () =>
        {
            return "Hello World Joakin!";
        });
    }
}