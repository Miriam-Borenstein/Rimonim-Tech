namespace MeterSystem.Api.src.Features.Readings;

public class ReadingsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/readings", async (ReadingRequest request, ReadingsHandler handler) =>
        {
            if (request.meter_number <= 0)
                return Results.BadRequest();

            if (request.Readings == null)
                return Results.BadRequest();

            await handler.Handle(request);

            return Results.Accepted();
        });
    }
}
