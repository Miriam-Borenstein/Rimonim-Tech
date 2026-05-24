using System.Text;
using System.Text.Json;
using MeterSystem.Shared.src.Messaging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace MeterSystem.Api.src.Messaging;

public class RabbitMqPublisher : IMessagePublisher, IDisposable
{
    private readonly RabbitMqOptions _options;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqPublisher(IOptions<RabbitMqOptions> options)
    {
        _options = options.Value;

        var factory = new ConnectionFactory
        {
            HostName = _options.Host,
            Port = _options.Port,
            UserName = _options.User,
            Password = _options.Password
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: _options.QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        _channel.BasicPublish(
            exchange: "",
            routingKey: _options.QueueName,
            basicProperties: null,
            body: body);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();

        _channel?.Dispose();
        _connection?.Dispose();
    }
}
