using Chiro.Application;
using Chiro.Infrastructure;
using Chiro.Infrastructure.Persistence.Seed;
using Chiro.Infrastructure.Seed;
using Chiro.Presentation;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Use the type of any class that lives in your Presentation project
builder.Services.AddControllers();
    //.AddApplicationPart(typeof(Chiro.Presentation.Controllers.AuthController).Assembly);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register layers
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    await DbSeeder.SeedAsync(app.Services);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
