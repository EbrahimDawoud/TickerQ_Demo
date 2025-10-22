
using TickerQ.Utilities.Base;
using TickerQ.Utilities.Models;

namespace Application.Jobs;

public class ExceptionJob
{

    [TickerFunction("TestException", "*/1 * * * *")]
    public async Task TestException(TickerFunctionContext context, CancellationToken cancellationToken)
    {
        Console.WriteLine($" Exception test started at {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}");
        await Task.Delay(1000, cancellationToken);
        throw new InvalidOperationException("Test exception for TickerQ exception handler!");
    }
}
