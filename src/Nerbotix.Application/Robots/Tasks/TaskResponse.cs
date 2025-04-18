﻿namespace Nerbotix.Application.Robots.Tasks;

public class TaskResponse : TaskBaseResponse
{
    public IList<TaskDataResponse>? Data { get; set; }
    public IList<TaskRequirementResponse>? Requirements { get; set; }
    public IList<LogResponse> Logs { get; set; } = [];
    public string? ArchivePath { get; set; }
    public IList<TaskFileResponse>? Files { get; set; }
}