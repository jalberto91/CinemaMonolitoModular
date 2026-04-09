using System.Reflection;
using System.Text;

using CP.Portal.Movies.Module;
using CP.Portal.Movies.Module.Data;
using CP.Portal.Users.Module;
using CP.Portal.Users.Module.Data;

using FastEndpoints;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

List<Assembly> mediatorAssemblies = [typeof(Program).Assembly];

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMovieServices(builder.Configuration);
builder.Services.AddUserModuleServices(builder.Configuration, mediatorAssemblies);
builder.Services.AddOpenApi();
builder.Services.AddFastEndpoints();

var jwtSecret = builder.Configuration["Auth:JwtSecret"]!;
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

await using ( var scope = app.Services.CreateAsyncScope())
{
    var movieDbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
    await movieDbContext.Database.MigrateAsync();

    var userDbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    await userDbContext.Database.MigrateAsync();
}

app.UseFastEndpoints();

app.Run();
