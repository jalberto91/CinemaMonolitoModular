using CP.Portal.Movies.Module;

using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMovieServices(builder.Configuration);
builder.Services.AddFastEndpoints();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.UseFastEndpoints();

app.Run();
