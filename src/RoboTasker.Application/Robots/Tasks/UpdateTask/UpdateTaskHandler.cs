﻿using System.IO.Compression;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RoboTasker.Application.Common.Abstractions;
using RoboTasker.Application.Common.Errors.Robots;
using RoboTasker.Domain.Capabilities;
using RoboTasker.Domain.Repositories.Abstractions;
using RoboTasker.Domain.Services;
using RoboTasker.Domain.Tasks;
using RoboTasker.Domain.Tasks.Data;

namespace RoboTasker.Application.Robots.Tasks.UpdateTask;

public class UpdateTaskHandler(
    ICurrentUser currentUser,
    ITenantRepository<RobotTask> taskRepository,
    ITenantRepository<Capability> capabilityRepository) : ICommandHandler<UpdateTaskCommand, TaskBaseResponse>
{
    public async Task<ErrorOr<TaskBaseResponse>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetAsync(
            c => c.Id == request.Id,
            q => q
                .Include(t => t.Properties)
                .Include(t => t.Requirements)
                .Include(t => t.TaskData)
                .Include(t => t.Archive),
            cancellationToken: cancellationToken);
        
        if (task == null)
        {
            return Error.NotFound(TaskErrors.NotFound, TaskErrors.NotFoundDescription);
        }


        if (!string.IsNullOrEmpty(request.Name))
        {
            task.Name = request.Name;
        }

        task.Description = request.Description;

        if (request.Priority.HasValue)
        {
            task.Priority = request.Priority.Value;
        }

        if (request.Complexity.HasValue)
        {
            task.Complexity = request.Complexity.Value;
        }

        if (request.EstimatedDuration.HasValue)
        {
            task.EstimatedDuration = request.EstimatedDuration.Value;
        }
        
        
        foreach (var deleteProperty in request.DeletedProperties ?? [])
        {
            var property = task.Properties.FirstOrDefault(p => p.Id == deleteProperty);
            if (property != null)
            {
                task.Properties.Remove(property);
            }
        }
        
        foreach (var deleteRequirement in request.DeletedRequirements ?? [])
        {
            var requirement = task.Requirements.FirstOrDefault(p => p.CapabilityId == deleteRequirement);
            if (requirement != null)
            {
                task.Requirements.Remove(requirement);
            }
        }
        
        foreach (var deleteData in request.DeletedData ?? [])
        {
            var data = task.TaskData.FirstOrDefault(p => p.Id == deleteData);
            if (data != null)
            {
                task.TaskData.Remove(data);
            }
        }
        
        foreach (var newProperty in request.Properties ?? [])
        {
            if (newProperty.ExistingId.HasValue)
            {
                var existingProp = task.Properties.FirstOrDefault(
                    p => p.Id == newProperty.ExistingId.Value && p.Value != newProperty.Value.ToString());
                if (existingProp != null)
                {
                    existingProp.Value = newProperty.Value.ToString() ?? string.Empty;
                }
            }
            else
            {
                var property = new RobotTaskProperty
                {
                    Key = newProperty.Key,
                    Value = newProperty.Value.ToString()!,
                };

                task.Properties.Add(property);
            }
        }
        
        foreach (var requirement in request.Requirements ?? [])
        {
            if (requirement.ExistingId.HasValue)
            {
                var existingReq = task.Requirements.FirstOrDefault(
                    p => p.Id == requirement.ExistingId.Value && p.RequiredLevel != requirement.Level);
                if (existingReq != null)
                {
                    existingReq.RequiredLevel = requirement.Level;
                }
            }
            else
            {
                var capability = await capabilityRepository.GetAsync(
                    c => c.Id == requirement.CapabilityId,
                    cancellationToken: cancellationToken);

                if (capability == null)
                {
                    continue;
                }

                var newRequirement = new RobotTaskRequirement
                {
                    CapabilityId = capability.Id,
                    RequiredLevel = requirement.Level,
                };

                task.Requirements.Add(newRequirement);
            }
        }
        
        foreach (var data in request.Data ?? [])
        {
            if (data.ExistingId.HasValue)
            {
                var existingData = task.TaskData.FirstOrDefault(
                    p => p.Id == data.ExistingId.Value && (p.Type != data.Type || p.Value != data.Value));
                if (existingData != null)
                {
                    existingData.Value = data.Value?.ToString() ?? string.Empty;
                    existingData.Type = data.Type;
                }
            }
            else
            {
                var newData = new RobotTaskData
                {
                    Key = data.Key,
                    Value = data.Value?.ToString()!,
                };

                task.TaskData.Add(newData);
            }
        }

        if ((request.DeletedFiles != null || request.Files != null) && task.Archive != null)
        {
            var deletedFiles = request.DeletedFiles?.ToArray() ?? [];
            var newArchive = await UpdateArchiveAsync(task.Archive.Url, deletedFiles, request.Files);

            if (newArchive != null)
            {
                task.Archive.Url = newArchive.Url;
                task.Archive.FileName = Path.GetFileName(newArchive.Url);
                task.Archive.Size = new FileInfo(newArchive.Url).Length;
            }
        }
        
        var updatedTask = await taskRepository.UpdateAsync(task, cancellationToken);
        
        return new TaskBaseResponse
        {
            Id = updatedTask.Id,
            TenantId = updatedTask.TenantId,
        };
    }

    private async Task<RobotTaskFiles?> UpdateArchiveAsync(string path, string[]? deleteFiles, IFormFileCollection? createFiles)
    {
        var userId = currentUser.GetUserId();
        var directory = Path.Combine(Path.GetTempPath(), "Archives", userId!.Value.ToString());

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Archive not found: {path}");
        }

        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);

        try
        {
            ZipFile.ExtractToDirectory(path, tempDir, true);

            if (deleteFiles != null)
            {
                foreach (var deleteFile in deleteFiles)
                {
                    var filePath = Path.Combine(tempDir, deleteFile);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
            }

            if (createFiles != null)
            {
                foreach (var file in createFiles)
                {
                    var newFilePath = Path.Combine(tempDir, file.FileName);
                    await using var stream = new FileStream(newFilePath, FileMode.Create);
                    await file.CopyToAsync(stream);
                }
            }

            var newArchivePath = Path.Combine(directory, $"{Guid.NewGuid()}.zip");

            ZipFile.CreateFromDirectory(tempDir, newArchivePath, CompressionLevel.Optimal, false);

            Directory.Delete(tempDir, true);

            return new RobotTaskFiles
            {
                Url = newArchivePath,
                FileName = Path.GetFileName(newArchivePath),
                Size = new FileInfo(newArchivePath).Length,
            };
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating archive: {e.Message}");
            return null;
        }
    }
}