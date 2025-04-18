﻿using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Nerbotix.Application.Common.Abstractions;
using Nerbotix.Application.Common.Errors.Tenants;
using Nerbotix.Application.Roles.Permissions;
using Nerbotix.Domain.Repositories.Abstractions;
using Nerbotix.Domain.Tenants;

namespace Nerbotix.Application.Roles.Roles.GetRoleById;

public class GetRoleByIdHandler(ITenantRepository<Role> roleRepository) : IQueryHandler<GetRoleByIdQuery, RoleResponse>
{
    public async Task<ErrorOr<RoleResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetWithSelectorAsync(
            r => new RoleResponse
            {
                Id = r.Id,
                Name = r.Name!,
                IsSystem = r.IsSystem,
                Permissions = r.Permissions
                    .Select(p => new PermissionBaseResponse
                    {
                        Id = p.PermissionId,
                        Name = p.Permission.Name,
                        IsSystem = p.Permission.IsSystem,
                        GroupName = p.Permission.Group.Name
                    }).ToList()
            },
            r => r.Id == request.Id,
            q => q
                .Include(r => r.Permissions)
                .ThenInclude(p => p.Permission)
                .ThenInclude(p => p.Group),
            cancellationToken);

        if (role == null)
        {
            return Error.NotFound(RoleErrors.NotFound, RoleErrors.NotFoundDescription);
        }

        return role;
    }
}