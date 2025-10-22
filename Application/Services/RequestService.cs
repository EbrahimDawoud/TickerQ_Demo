using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services;

public class RequestService(IRepository<RequestLog> logRepository, IRepository<RequestEntity> repository)
{    
    public async Task<RequestEntity> CreateAsync(string title)
    {
        var request = new RequestEntity
        {
            Title = title,
            Status = RequestStatus.Created
        };

        await repository.AddAsync(request);
        await repository.SaveAsync();

        // Log creation
        var log = new RequestLog
        {
            RequestId = request.Id,
            Action = "Request created"
        };
        await logRepository.AddAsync(log);
        await logRepository.SaveAsync();

        return request;
    }

    public async Task<List<RequestEntity>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<RequestDto?> UpdateStatusAsync(Guid requestId, RequestStatus newStatus)
    {
        var request = await repository.GetAsync(r => r.Id == requestId);
        if (request == null) return null;

        request.Status = newStatus;

        switch (newStatus)
        {
            case RequestStatus.Approved: request.ApprovedAt = DateTime.UtcNow; break;
            case RequestStatus.Completed: request.CompletedAt = DateTime.UtcNow; break;
            case RequestStatus.Rejected: request.RejectedAt = DateTime.UtcNow; break;
        }

        repository.Update(request);

        var log = new RequestLog
        {
            RequestId = request.Id,
            Action = $"Status changed to {newStatus}"
        };
        await logRepository.AddAsync(log);

        await repository.SaveAsync();
        await logRepository.SaveAsync();

        return new RequestDto(
            request.Id,
            request.Title,
            request.Status,
            request.CreatedAt,
            request.ApprovedAt,
            request.CompletedAt,
            request.RejectedAt
        );
    }
    public async Task<List<RequestLog>> GetLogsAsync(Guid requestId)
    {
        return await logRepository.GetAllAsync(l => l.RequestId == requestId);
    }

}
