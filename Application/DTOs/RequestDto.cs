
using Domain.Enums;

namespace Application.DTOs;


public record RequestDto
    (
    Guid Id,
    string Title,
    RequestStatus Status,
    DateTime CreatedAt,
    DateTime? ApprovedAt,
    DateTime? CompletedAt,
    DateTime? RejectedAt

    );

public record RequestLogsDto
    (
    Guid Id,
    string Title,
    RequestStatus Status,
    DateTime CreatedAt,
    DateTime? ApprovedAt,
    DateTime? CompletedAt,
    DateTime? RejectedAt,
    List<RequestLogDto> Logs

    );