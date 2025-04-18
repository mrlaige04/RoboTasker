﻿using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Nerbotix.Application.Common.Behaviours;

namespace Nerbotix.Application;

public static class RegisterDependencies
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TenantAuthBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });
    }
}