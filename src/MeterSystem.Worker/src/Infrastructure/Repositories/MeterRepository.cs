
using Dapper;
using MeterSystem.Worker.src.Infrastructure.Data;

namespace MeterSystem.Worker.src.Infrastructure.Repositories;

public class MeterRepository : IMeterRepository
{
    private readonly DbConnectionFactory _db;

    public MeterRepository(DbConnectionFactory db)
    {
        _db = db;
    }
    public async Task<long> GetOrCreateMeterAsync(long meterNumber)
    {
        using var connection = _db.Create();
        await connection.OpenAsync();

        var existingId = await connection.QueryFirstOrDefaultAsync<long?>(
            @"SELECT meter_id
              FROM meters 
              WHERE meter_number = @meterNumber",
               new { meterNumber });

        if (existingId.HasValue)

            return existingId.Value;

        return await connection.QuerySingleAsync<long>(
           @"INSERT INTO meters (meter_number) VALUES (@meterNumber) RETURNING meter_id",
           new { meterNumber });
                      
    }
}
