using Microsoft.Extensions.Logging;
using TickerQ.Utilities.Base;
using TickerQ.Utilities.Enums;
using TickerQ.Utilities.Interfaces;

namespace TickerQ_Demo.Services;

public class TickerExceptionHandler(ILogger<TickerExceptionHandler> logger) : ITickerExceptionHandler
{
    public async Task HandleExceptionAsync(Exception exception, Guid tickerId, TickerType tickerType)
    {
        Console.WriteLine($" EXCEPTION - ID: {tickerId}, Type: {tickerType}, Error: {exception.Message}");
        logger.LogError(exception, "!!!!!!!!!!!!!!!!!!!!!!TickerQ job failed - ID: {TickerId}, Type: {TickerType}", tickerId, tickerType);
        await Task.CompletedTask;
    }

    public async Task HandleCanceledExceptionAsync(Exception exception, Guid tickerId, TickerType tickerType)
    {
        Console.WriteLine($" CANCELLED - ID: {tickerId}, Type: {tickerType}, Reason: {exception.Message}");
        logger.LogWarning("TickerQ job cancelled - ID: {TickerId}, Type: {TickerType}", tickerId, tickerType);
        await Task.CompletedTask;
    }
}
