using Azure.Identity;
using Azure.Storage.Blobs;
using Chiro.Application;
using Chiro.Infrastructure;
using Chiro.Infrastructure.Persistence.Seed;
using Chiro.Infrastructure.Seed;
using Chiro.Presentation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Use the type of any class that lives in your Presentation project
builder.Services.AddControllers();
    //.AddApplicationPart(typeof(Chiro.Presentation.Controllers.AuthController).Assembly);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//register blobserviceclient
builder.Services.AddSingleton(x =>
{
    var accountUrl = builder.Configuration["AzureBlobStorage:AccountUrl"];
    var containerName = builder.Configuration["AzureBlobStorage:ContainerName"];

    return new BlobContainerClient(
        new Uri($"{accountUrl}/{containerName}"),
        new DefaultAzureCredential());
});

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
