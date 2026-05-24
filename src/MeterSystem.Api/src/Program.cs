using Carter;
using MeterSystem.Api.src.Features.Readings;
using MeterSystem.Api.src.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter();

builder.Services.AddScoped<ReadingsHandler>();

builder.Services.AddRabbitMqPublisher(builder.Configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.MapCarter();

app.Run();
