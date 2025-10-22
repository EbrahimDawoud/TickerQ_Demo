namespace Domain.Entities;

public class JobEntity
{
    public Guid Id { get; set; }
    public string JobType { get; set; } = default!;
    public string Parameters { get; set; } = default!;
    public string Status { get; set; } = default!; // Queued, Running, Completed, Failed
    public int RetryCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? Error { get; set; }
    public DateTime? ScheduledFor { get; set; }
}
