using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ_Consumer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RabbitMQ_Consumer.Services;
using SharedLibrary;
using RabbitMQ_Producer.Services;
using RabbitMQ_Producer.Helper.Extensions;
using RabbitMQ_Consumer.Repositories;
using Microsoft.Extensions.Logging;


var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.None); // Disable EF Core command logs



// Register services
builder.Services.AddTransient<IRabbitMQConsumer, RabbitMQConsumer>();
builder.Services.AddSingleton<LogSystemService>();

// Register AutoMapper
builder.Services.AddApplicationMappings();

builder.Services.AddApplicationServices();
builder.Services.AddScoped<LogSystemService>();

builder.Services.AddApplicationRepositories();
builder.Services.AddScoped<LogSystemRepository>();

// connection database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Register Controller
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.WriteIndented = true; // Format JSON
});


var app = builder.Build();


// Resolve the consumer and use it
var rabbitMQConsumer = app.Services.GetRequiredService<IRabbitMQConsumer>();



// Example usage
rabbitMQConsumer.ReceiveLogMessage();

app.Run();