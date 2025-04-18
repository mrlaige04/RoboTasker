﻿using Nerbotix.Application.Roles.Permissions;
using Nerbotix.Application.Roles.Roles;

namespace Nerbotix.Application.User;

public class CurrentUserResponse
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string TenantName { get; set; } = string.Empty;
    public string Email { get; set; } = null!;
    public string? AvatarUrl { get; set; }
    public IList<PermissionBaseResponse> Permissions { get; set; } = [];
    public IList<RoleBaseResponse> Roles { get; set; } = [];
}