﻿namespace RoboTasker.Application.Common.Errors.Tenants;

public static class PermissionErrors
{
    public const string NotFound = "Permission.NotFound";
    public const string NotFoundDescription = "Permission not found.";
    
    public const string GroupNotFound = "PermissionGroup.NotFound";
    public const string GroupNotFoundDescription = "Permission group not found.";
    
    public const string Conflict = "Permission.Conflict";
    public const string ConflictDescription = "Permission with this name already exists.";
    
    public const string ConflictGroup = "PermissionGroup.Conflict";
    public const string ConflictGroupDescription = "Permission group with this name already exists.";
}