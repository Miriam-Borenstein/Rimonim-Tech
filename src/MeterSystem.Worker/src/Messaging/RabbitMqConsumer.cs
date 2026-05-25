using MeterSystem.Shared.src.Messaging;

namespace MeterSystem.Worker.src.Messaging;

public class RabbitMqConsumer
{
    private readonly RabbitMqOptions _options;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ReadingHandler _handler;
    private readonly ILogger<RabbitMqConsumer> _logger;

    public RabbitMqConsumer(IOptions<RabbitMqOptions> options , ReadingHandler handler, ILogger<RabbitMqConsumer> logger)
    {
        _options = options.Value;
        _handler = handler;
        _logger = logger;

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

    public async Task Start(CancellationToken cancellationToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (s, ea) =>
        {
            await OnMessageReceived(s, ea);
        };

        cancellationToken.Register(() =>
        {
            _channel.Close();
            _connection.Close();
        });

        _channel.BasicConsume(_options.QueueName, autoAck: false, consumer);
    }

    private async Task OnMessageReceived(object? sender, BasicDeliverEventArgs ea)
    {
        try
        {
            var json = Encoding.UTF8.GetString(ea.Body.ToArray());
            var msg = JsonSerializer.Deserialize<ReadingMessage>(json);

            if (msg != null)
                await _handler.Handle(msg);

            _channel.BasicAck(ea.DeliveryTag, false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            _channel.BasicNack(ea.DeliveryTag, false, true);
        }


    }
}
