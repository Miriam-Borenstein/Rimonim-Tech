using MeterSystem.Shared.src.Messaging;

namespace MeterSystem.Worker.src.Infrastructure.Repositories;

public interface IReadingRepository
{
    Task InsertReadingAsync(long meterId, ReadingItem readingItem);
}
