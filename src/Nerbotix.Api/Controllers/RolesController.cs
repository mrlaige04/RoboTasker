﻿
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nerbotix.Api.Models.Tenants;
using Nerbotix.Application.Roles.Roles.CreateRole;
using Nerbotix.Application.Roles.Roles.DeleteRole;
using Nerbotix.Application.Roles.Roles.GetRoleById;
using Nerbotix.Application.Roles.Roles.GetRoles;
using Nerbotix.Application.Roles.Roles.UpdateRole;

namespace Nerbotix.Api.Controllers;

[Route("[controller]"), Authorize]
public class RolesController(IMediator mediator) : BaseController
{
    [HttpPost("")]
    public async Task<IActionResult> CreateRole(CreateRoleCommand command)
    {
        var result = await mediator.Send(command);
        return result.Match(Ok, Problem);
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllRoles([FromQuery] GetRolesQuery query)
    {
        var result = await mediator.Send(query);
        return result.Match(Ok, Problem);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRoleById(Guid id)
    {
        var query = new GetRoleByIdQuery(id);
        var result = await mediator.Send(query);
        return result.Match(Ok, Problem);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateRole(Guid id, UpdateRoleRequest request)
    {
        var command = request.Adapt<UpdateRoleCommand>();
        command.Id = id;
        var result = await mediator.Send(command);
        return result.Match(Ok, Problem);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRole(Guid id)
    {
        var command = new DeleteRoleCommand(id);
        var result = await mediator.Send(command);
        return result.Match(Ok, Problem);
    }
}