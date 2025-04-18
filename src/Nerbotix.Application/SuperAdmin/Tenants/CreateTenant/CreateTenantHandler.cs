﻿using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nerbotix.Application.Common.Abstractions;
using Nerbotix.Application.Common.Data;
using Nerbotix.Application.Common.Emails;
using Nerbotix.Application.Common.Errors;
using Nerbotix.Domain.Consts;
using Nerbotix.Domain.Repositories.Abstractions;
using Nerbotix.Domain.Services;
using Nerbotix.Domain.Tenants;
using Nerbotix.Domain.Tenants.Settings;

namespace Nerbotix.Application.SuperAdmin.Tenants.CreateTenant;

public class CreateTenantHandler(
    UserManager<Domain.Tenants.User> userManager,
    ITenantRepository<Role> roleRepository, 
    ITenantSeeder seeder,
    ICurrentUser currentUser,
    ITenantRepository<Domain.Tenants.User> userRepository,
    IBaseRepository<Tenant> tenantRepository, IUserEmailSender userEmailSender) 
    : ICommandHandler<CreateTenantCommand, TenantBaseResponse>
{
    public async Task<ErrorOr<TenantBaseResponse>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
    {
        if (await tenantRepository.ExistsAsync(t => 
                    EF.Functions.Like(t.Email, $"%{request.Email}%") || 
                    EF.Functions.Like(t.Name, $"{request.Name}"),
                cancellationToken: cancellationToken))
        {
            return Error.Conflict(TenantErrors.Conflict, TenantErrors.ConflictDescription);
        }

        if (await userManager.FindByEmailAsync(request.Email) != null)
        {
            return Error.Conflict(TenantErrors.Conflict, "User with the same email already exists.");
        }

        var tenantId = Guid.NewGuid();
        currentUser.SetTenantId(tenantId);
        
        var tenant = new Tenant
        {
            Id = tenantId,
            Name = request.Name,
            Email = request.Email,
            Settings = TenantSettings.CreateDefault(tenantId)
        };
        
        var createdTenant = await tenantRepository.AddAsync(tenant, cancellationToken);

        currentUser.SetTenantId(createdTenant.Id);
        await seeder.SeedRolesAndPermissionsAsync(createdTenant.Id);
        
        var user = Domain.Tenants.User.Create(request.Email);
        user.TenantId = createdTenant.Id;
        await userManager.CreateAsync(user);
        
        var adminRole = await roleRepository.GetAsync(
            r => r.Name == RoleNames.Admin,
            q => q.IgnoreQueryFilters(),
            cancellationToken: cancellationToken);
        
        user.Roles.Add(new UserRole
        {
            Role = adminRole!,
            TenantId = createdTenant.Id
        });
        
        await userRepository.UpdateAsync(user, cancellationToken);
        
        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        await userEmailSender.SendRegistrationEmail(user, token);
        
        return new TenantBaseResponse
        {
            Id = createdTenant.Id,
            Name = createdTenant.Name,
            Email = createdTenant.Email,
        };
    }
}