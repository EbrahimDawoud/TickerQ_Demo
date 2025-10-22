
namespace Domain.Entities;

public class RequestLog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid RequestId { get; set; }
    public string Action { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public RequestEntity Request { get; set; } = default!;
}
