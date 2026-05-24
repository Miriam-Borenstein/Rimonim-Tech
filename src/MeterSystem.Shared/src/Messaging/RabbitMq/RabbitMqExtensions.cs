using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeterSystem.Shared.src.Messaging.RabbitMq;

public static class MessagingExtensions
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<RabbitMqOptions>(
            config.GetSection("RabbitMq"));

        services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();

        return services;
    }
}

