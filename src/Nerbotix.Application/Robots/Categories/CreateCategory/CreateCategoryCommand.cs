﻿using Nerbotix.Application.Common.Abstractions;
using Nerbotix.Domain.Robots;

namespace Nerbotix.Application.Robots.Categories.CreateCategory;

public class CreateCategoryCommand : ITenantCommand<CategoryBaseResponse>
{
    public string Name { get; set; } = null!;
    public IList<CreateCategoryCommandPropertyItem> Properties { get; set; } = [];
}

public class CreateCategoryCommandPropertyItem
{
    public string Name { get; set; } = null!;
    public RobotPropertyType Type { get; set; }
    public string? Unit { get; set; }
}