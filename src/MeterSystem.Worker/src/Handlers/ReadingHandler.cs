using MeterSystem.Shared.src.Messaging;
using MeterSystem.Worker.src.Infrastructure.Repositories;

namespace MeterSystem.Worker.src.Handlers
{
    public class ReadingHandler
    {
        private readonly IMeterRepository _meterRepo;
        private readonly IReadingRepository _readingRepo;
        private readonly ILogger<ReadingHandler> _logger;

        public ReadingHandler(IMeterRepository meterRepo, IReadingRepository readingRepo, ILogger<ReadingHandler> logger)
        {
            _meterRepo = meterRepo;
            _readingRepo = readingRepo;
            _logger = logger;
        }
        public async Task Handle(ReadingMessage msg)
        {
            _logger.LogInformation("Message received from queue");

            var meterId = await _meterRepo.GetOrCreateMeterAsync(msg.MeterNumber);

            foreach (var reading in msg.Readings)
            {
                await _readingRepo.InsertReadingAsync(meterId, reading);
            }
            _logger.LogInformation("Saving to DB");
        }

    }
}
