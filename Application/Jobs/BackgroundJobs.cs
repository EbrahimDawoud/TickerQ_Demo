using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Logging;
using TickerQ.Utilities.Base;
using TickerQ.Utilities.Models;

namespace Application.Jobs;

public class BackgroundJobs(IRepository<RequestEntity> _requestRepository, IRepository<RequestLog> _logRepository, IRepository<Report> _reportRepository, ILogger<RequestEntity> logger)
{
    [TickerFunction("AutoReject", "*/30 * * * *")]
    public async Task RejectStaleApprovedRequests(CancellationToken cancellationToken)
    {
        // Get all approved requests that are older than 10 seconds (for testing)
        var tenSecondsAgo = DateTime.UtcNow.AddSeconds(-10);
        var staleApprovedRequests = await _requestRepository.GetAllAsync(r =>
            r.Status == RequestStatus.Approved &&
            r.ApprovedAt.HasValue &&
            r.ApprovedAt.Value < tenSecondsAgo);
        Console.WriteLine("The Background Job is running //////////////////////////////////////");
        foreach (var request in staleApprovedRequests)
        {
            try
            {
                await UpdateRequestStatus(request.Id, RequestStatus.Rejected, "Auto-rejected: Approved for more than 10 seconds without completion");
                Console.WriteLine("//////////////////there change in the status ");
            }
            catch (Exception ex)
            {
                await LogError(request.Id, $"Error rejecting stale request: {ex.Message}");
            }
        }
    }
   
    
    //[TickerFunction(functionName:"RequestReport", cronExpression:"0 * * * *")]
    [TickerFunction(functionName:"CreateReport")]
    public async Task CreateReport(TickerFunctionContext tickerContext, CancellationToken cancellationToken)
    {
        var report = new Report
        {
            Title = $"Scheduled Report - {DateTime.UtcNow:yyyy-MM-dd HH:mm}",
            Content = $"This is an automatically generated report created at {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}",
            CreatedAt = DateTime.UtcNow
        };

        await _reportRepository.AddAsync(report);
        await _reportRepository.SaveAsync();
        logger.LogInformation($"Report created with ID: {report.Id}");
    }

    private async Task UpdateRequestStatus(Guid requestId, RequestStatus newStatus, string reason)
    {
        var request = await _requestRepository.GetAsync(r => r.Id == requestId);
        if (request == null) return;

        request.Status = newStatus;
        
        switch (newStatus)
        {
            case RequestStatus.Approved: request.ApprovedAt = DateTime.UtcNow; break;
            case RequestStatus.Completed: request.CompletedAt = DateTime.UtcNow; break;
            case RequestStatus.Rejected: request.RejectedAt = DateTime.UtcNow; break;
        }

        _requestRepository.Update(request);

        // Log the status change
        var log = new RequestLog
        {
            RequestId = requestId,
            Action = $"Status changed to {newStatus}: {reason}"
        };
        await _logRepository.AddAsync(log);
        await _logRepository.SaveAsync();
    }

    private async Task LogError(Guid requestId, string errorMessage)
    {
        var log = new RequestLog
        {
            RequestId = requestId,
            Action = $"Error: {errorMessage}"
        };
        await _logRepository.AddAsync(log);
        await _logRepository.SaveAsync();
    }
}
