﻿using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Nerbotix.Application.Common.Abstractions;
using Nerbotix.Application.Common.Errors;
using Nerbotix.Application.Common.Errors.Robots;
using Nerbotix.Domain.Repositories.Abstractions;
using Nerbotix.Domain.Robots;
using Nerbotix.Domain.Services;
using Nerbotix.Domain.Tenants;

namespace Nerbotix.Application.Robots.Categories.CreateCategory;

public class CreateCategoryHandler(
    ICurrentUser currentUser,
    IBaseRepository<Tenant> tenantRepository,
    ITenantRepository<RobotCategory> categoryRepository): ICommandHandler<CreateCategoryCommand, CategoryBaseResponse>
{
    public async Task<ErrorOr<CategoryBaseResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var tenantId = currentUser.GetTenantId();
        if (!await tenantRepository.ExistsAsync(t => t.Id == tenantId, cancellationToken: cancellationToken))
        {
            return Error.NotFound(TenantErrors.NotFound, TenantErrors.NotFoundDescription);
        }

        if (await categoryRepository.ExistsAsync(c => 
                c.Name == request.Name, cancellationToken: cancellationToken))
        {
            return Error.Conflict(CategoryErrors.Conflict, CategoryErrors.ConflictDescription);   
        }
        
        var properties = request.Properties
            .Select(p => new RobotProperty
            {
                Name = p.Name,
                Type = p.Type,
                Unit = p.Unit,
            })
            .ToList();
        
        var robotCategory = new RobotCategory
        {
            Name = request.Name,
            Properties = properties
        };
        
        var createdCategory = await categoryRepository.AddAsync(robotCategory, cancellationToken);
        
        return new CategoryBaseResponse
        {
            Id = createdCategory.Id,
            Name = createdCategory.Name,
            TenantId = createdCategory.TenantId,
            CreatedAt = createdCategory.CreatedAt,
            UpdatedAt = createdCategory.UpdatedAt,
        };
    }
}