﻿using ErrorOr;
using Microsoft.EntityFrameworkCore;
using RoboTasker.Application.Common.Abstractions;
using RoboTasker.Domain.Abstractions;
using RoboTasker.Domain.Capabilities;
using RoboTasker.Domain.Repositories.Abstractions;

namespace RoboTasker.Application.Robots.Capabilities.GetCapabilitiesGroups;

public class GetCapabilitiesGroupsHandler(
    ITenantRepository<CapabilityGroup> capabilityGroupRepository) : IQueryHandler<GetCapabilitiesGroupQuery, PaginatedList<CapabilityBaseResponse>>
{
    public async Task<ErrorOr<PaginatedList<CapabilityBaseResponse>>> Handle(GetCapabilitiesGroupQuery request, CancellationToken cancellationToken)
    {
        var capabilities = await capabilityGroupRepository.GetAllWithSelectorPaginatedAsync(
            request.PageNumber, request.PageSize,
            c => new CapabilityBaseResponse
            {
                Id = c.Id,
                Name = c.Name,
                GroupName = c.Name,
                Description = c.Description,
                CapabilitiesCount = c.Capabilities.Count
            },
            include: q => q.Include(c => c.Capabilities),
            cancellationToken: cancellationToken);
        
        return capabilities;
    }
}