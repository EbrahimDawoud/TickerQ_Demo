using Microsoft.Extensions.Logging;
using TickerQ.Utilities.Base;
namespace Application.Jobs;

public class CronJob(ILogger<CronJob> logger)
{
    [TickerFunction("CleanUP", "*/1 * * * *")]
    public void CleanUP()
    {
        logger.LogInformation("***************Cron Job is running - CleanUP executed at: {time}", DateTimeOffset.Now);
    }
}
