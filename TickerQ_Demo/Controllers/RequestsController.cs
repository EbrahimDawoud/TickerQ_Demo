using Application.DTOs;
using Application.Jobs;
using Application.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using TickerQ.Utilities;
using TickerQ.Utilities.Interfaces.Managers;
using TickerQ.Utilities.Models.Ticker;

namespace TickerQ_Demo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RequestsController(RequestService _service) : ControllerBase
{
    [HttpPost("create")]
    public async Task<ActionResult<RequestDto>> Create([FromBody] CreateRequestDto dto)
    {
        var entity = await _service.CreateAsync(dto.Title);

        var requestDto = new RequestDto(
            entity.Id,
            entity.Title,
            entity.Status,
            entity.CreatedAt,
            entity.ApprovedAt,
            entity.CompletedAt,
            entity.RejectedAt
        );

        return Ok(requestDto);
    }

    // Get all requests
    [HttpGet]
    public async Task<ActionResult<List<RequestDto>>> GetAll()
    {
        var entities = await _service.GetAllAsync();

        var dtoList = entities.Select(x => new RequestDto(
            x.Id,
            x.Title,
            x.Status,
            x.CreatedAt,
            x.ApprovedAt,
            x.CompletedAt,
            x.RejectedAt
        )).ToList();

        return Ok(dtoList);
    }

    [HttpPatch("{id}/status")]
    public async Task<ActionResult<RequestDto>> UpdateStatus(Guid id, [FromQuery] RequestStatus status)
    {
        var updatedDto = await _service.UpdateStatusAsync(id, status);

        if (updatedDto == null) return NotFound();

        return Ok(updatedDto);
    }

    [HttpGet("{id}/logs")]
    public async Task<ActionResult<List<RequestLogDto>>> GetLogs(Guid id)
    {
        var logs = await _service.GetLogsAsync(id);

        if (logs == null || !logs.Any())
            return NotFound("No logs found for this request.");

        var logDtos = logs.Select(l => new RequestLogDto(l.Action, l.CreatedAt)).ToList();

        return Ok(logDtos);
    }

    [HttpPost("Schedule")]
    public async Task<ActionResult> ScheduleBackgroundJobs(Point point, ITimeTickerManager<TimeTicker> manager)
    {
        await manager.AddAsync(new TimeTicker
        {
            Request = TickerHelper.CreateTickerRequest(point),
            ExecutionTime = DateTime.UtcNow.AddSeconds(10),
            Function = nameof(JobWithData.CleanUp),
            Retries = 3,
            RetryIntervals = [1, 2, 3],
            Description = "Job with data example"
        });
        return Ok("Background job scheduled.");

    }
}