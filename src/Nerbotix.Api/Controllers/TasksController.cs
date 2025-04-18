﻿using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nerbotix.Api.Models.Tasks;
using Nerbotix.Application.Robots.Tasks.CancelTask;
using Nerbotix.Application.Robots.Tasks.CreateTask;
using Nerbotix.Application.Robots.Tasks.DeleteTask;
using Nerbotix.Application.Robots.Tasks.GetTaskById;
using Nerbotix.Application.Robots.Tasks.GetTasks;
using Nerbotix.Application.Robots.Tasks.ReEnqueueTask;
using Nerbotix.Application.Robots.Tasks.UpdateTask;

namespace Nerbotix.Api.Controllers;

[Route("[controller]"), Authorize]
public class TasksController(IMediator mediator) : BaseController
{
    [HttpPost(""), DisableRequestSizeLimit]
    public async Task<IActionResult> CreateTask([FromForm] CreateTaskRequest request)
    {
        var command = request.Adapt<CreateTaskCommand>();
        command.Files = request.Files;
        var result = await mediator.Send(command);
        return result.Match(Ok, Problem);
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllTasks([FromQuery] GetTasksQuery query)
    {
        var result = await mediator.Send(query);
        return result.Match(Ok, Problem);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTaskById(Guid id)
    {
        var query = new GetTaskByIdQuery(id);
        var result = await mediator.Send(query);
        return result.Match(Ok, Problem);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTask(Guid id, [FromForm] UpdateTaskRequest request)
    {
        var command = request.Adapt<UpdateTaskCommand>();
        command.Files = request.Files;
        command.Id = id;
        var result = await mediator.Send(command);
        return result.Match(Ok, Problem);
    }

    [HttpPost("{id:guid}/enqueue")]
    public async Task<IActionResult> EnqueueTask(Guid id)
    {
        var command = new ReEnqueueTaskCommand(id);
        var result = await mediator.Send(command);
        return result.Match(Ok, Problem);
    }

    [HttpDelete("{id:guid}/status")]
    public async Task<IActionResult> CancelTask(Guid id)
    {
        var command = new CancelTaskCommand(id);
        var result = await mediator.Send(command);
        return result.Match(Ok, Problem);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        var command = new DeleteTaskCommand(id);
        var result = await mediator.Send(command);
        return result.Match(Ok, Problem);
    }
}