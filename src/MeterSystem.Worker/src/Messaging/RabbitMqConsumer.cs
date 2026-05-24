using MeterSystem.Shared.src.Messaging;

namespace MeterSystem.Worker.src.Messaging;

public class RabbitMqConsumer
{
    private readonly RabbitMqOptions _options;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ReadingHandler _handler;


    public RabbitMqConsumer(IOptions<RabbitMqOptions> options , ReadingHandler handler)
    {
        _options = options.Value;
        _handler = handler;

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

    public async Task Start()
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (s, ea) =>
        {
            await OnMessageReceived(s, ea);
        };

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
            Console.WriteLine(ex.Message);
            _channel.BasicNack(ea.DeliveryTag, false, true);
        }


    }
}
