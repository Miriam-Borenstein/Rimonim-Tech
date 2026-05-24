using MeterSystem.Shared.src.Messaging;
using MeterSystem.Worker.src.Infrastructure.Repositories;

namespace MeterSystem.Worker.src.Handlers
{
    public class ReadingHandler
    {
        private readonly IMeterRepository _meterRepo;
        private readonly IReadingRepository _readingRepo;

        public ReadingHandler(IMeterRepository meterRepo, IReadingRepository readingRepo)
        {
            _meterRepo = meterRepo;
            _readingRepo = readingRepo;
        }
        public async Task Handle(ReadingMessage msg)
        {

            var meterId = await _meterRepo.GetOrCreateMeterAsync(msg.MeterNumber);

            foreach (var reading in msg.Readings)
            {
                await _readingRepo.InsertReadingAsync(meterId, reading);
            }

        }

    }
}
