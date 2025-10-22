using Domain.Enums;

namespace Domain.Entities;

public class RequestEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public RequestStatus Status { get; set; } = RequestStatus.Created;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ApprovedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? RejectedAt { get; set; }
    public ICollection<RequestLog> Logs { get; set; } = new List<RequestLog>();

}