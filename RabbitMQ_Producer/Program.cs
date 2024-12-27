using Microsoft.EntityFrameworkCore;
using RabbitMQ_Producer;
using RabbitMQ_Producer.Helper.Mappings;
using RabbitMQ_Producer.Infrastructor.RabbitMQ;
using AutoMapper;
using RabbitMQ_Producer.Helper.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;
using SharedLibrary;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// rabbit MQ
builder.Services.AddSingleton<IRabbitMQProducer, RabbitMQProducer>();

// connection database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}, ServiceLifetime.Scoped);



// Register AutoMapper
builder.Services.AddApplicationMappings();

// Register services configuration
//builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.WriteIndented = true; // Format JSON
});

// Register services
builder.Services.AddApplicationServices();
//builder.Services.AddScoped<ScheduledService>();

// Register Repository
builder.Services.AddApplicationRepositories();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// register swagger
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
