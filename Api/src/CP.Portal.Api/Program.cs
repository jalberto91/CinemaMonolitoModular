using CP.Portal.Movies.Module;
using CP.Portal.Movies.Module.Data;
using CP.Portal.Users.Module;
using CP.Portal.Users.Module.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMovieServices(builder.Configuration);
builder.Services.AddUserModuleServices(builder.Configuration);
builder.Services.AddFastEndpoints();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

await using ( var scope = app.Services.CreateAsyncScope())
{
    var movieDbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
    await movieDbContext.Database.MigrateAsync();

    var userDbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    await userDbContext.Database.MigrateAsync();
}

app.UseFastEndpoints();

app.Run();
