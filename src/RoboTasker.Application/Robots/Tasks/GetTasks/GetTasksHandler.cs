﻿using ErrorOr;
using RoboTasker.Application.Common.Abstractions;
using RoboTasker.Domain.Abstractions;
using RoboTasker.Domain.Repositories.Abstractions;
using RoboTasker.Domain.Tasks;

namespace RoboTasker.Application.Robots.Tasks.GetTasks;

public class GetTasksHandler(
    ITenantRepository<RobotTask> taskRepository) : IQueryHandler<GetTasksQuery, PaginatedList<TaskBaseResponse>>
{
    public async Task<ErrorOr<PaginatedList<TaskBaseResponse>>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await taskRepository.GetAllWithSelectorPaginatedAsync(
            request.PageNumber, request.PageSize,
            t => new TaskBaseResponse
            {
                Id = t.Id,
                TenantId = t.TenantId,
                Name = t.Name,
                Description = t.Description,
                Priority = t.Priority,
                CategoryId = t.CategoryId,
                Status = t.Status,
                AssignedRobotId = t.AssignedRobotId,
                Complexity = t.Complexity,
                CompletedAt = t.CompletedAt,
                EstimatedDuration = t.EstimatedDuration
            },
            cancellationToken: cancellationToken);

        return tasks;
    }
}