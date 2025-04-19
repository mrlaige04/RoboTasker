﻿namespace Nerbotix.Application.BackgroundJobs;

public interface IJobsService
{
    string EnqueueTask(Guid taskId);
    
    string GetNextTask(Guid robotId);
}