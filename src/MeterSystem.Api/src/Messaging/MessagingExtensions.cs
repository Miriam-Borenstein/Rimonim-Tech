using MeterSystem.Shared.src.Messaging;

namespace MeterSystem.Api.src.Messaging;

public static class MessagingExtensions
{
    public static IServiceCollection AddRabbitMqPublisher(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<RabbitMqOptions>(
            config.GetSection("RabbitMq"));

        services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();

        return services;
    }
}

