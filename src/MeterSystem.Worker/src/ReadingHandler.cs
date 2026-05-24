using MeterSystem.Shared.src.Messaging.RabbitMq;

namespace MeterSystem.Worker.src
{
    public class ReadingHandler
    {
        public Task Handle(ReadingMessage msg)
        {
            Console.WriteLine($"Meter: {msg.MeterNumber}");

            foreach (var r in msg.Readings)
            {
                Console.WriteLine($"{r.Timestamp} => {r.Value}");
            }

            return Task.CompletedTask;
        }
    }
}
