﻿namespace Nerbotix.Application.Roles.Permissions;

public class PermissionGroupResponse : PermissionGroupBaseResponse
{
    public IList<PermissionBaseResponse> Permissions { get; set; } = [];
}