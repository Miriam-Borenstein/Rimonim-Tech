using MeterSystem.Shared.src.Messaging;

namespace MeterSystem.Worker.src.Messaging;

public static class MessagingExtensions
{
    public static IServiceCollection AddRabbitMqConsumer(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<RabbitMqOptions>(
            config.GetSection("RabbitMq"));

        services.AddSingleton<RabbitMqConsumer>();

        return services;
    }
}
