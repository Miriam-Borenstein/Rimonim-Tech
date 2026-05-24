using MeterSystem.Shared.src.Messaging;
using MeterSystem.Worker.src.Infrastructure.Data;
using MeterSystem.Worker.src.Infrastructure.Repositories;
using MeterSystem.Worker.src.Messaging;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<DbConnectionFactory>();

builder.Services.AddSingleton<ReadingHandler>();

builder.Services.AddRabbitMqConsumer(builder.Configuration);

builder.Services.AddHostedService<MeterWorker>();

builder.Services.AddScoped<IMeterRepository, MeterRepository>();
builder.Services.AddScoped<IReadingRepository, ReadingRepository>();



var host = builder.Build();
host.Run();
