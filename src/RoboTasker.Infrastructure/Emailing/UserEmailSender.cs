﻿using RoboTasker.Application.Common.Emails;
using RoboTasker.Application.Services;
using RoboTasker.Domain.Tenants;

namespace RoboTasker.Infrastructure.Emailing;

public class UserEmailSender(TemplateService templateService, IEmailSender emailSender) : IUserEmailSender
{
    public async Task SendRegistrationEmail(User user, string token)
    {
        var link = new Uri($"http://localhost:4200/auth/register?email={user.Email}&token={token}");
        var placeholders = new Dictionary<string, string>()
        {
            { "link", link.ToString() }
        };
        
        var content = await templateService.RenderTemplate(TemplateNames.Register, placeholders);
        
        await emailSender.SendEmailAsync(user.Email!, "Welcome to RoboTasker!", content, true);
    }

    public async Task SendResetPasswordEmail(User user, string code)
    {
        var placeholders = new Dictionary<string, string>()
        {
            { "code", code }
        };
        
        var content = await templateService.RenderTemplate(TemplateNames.ForgotPassword, placeholders);
        await emailSender.SendEmailAsync(user.Email!, "Password Reset", content, true);
    }

    public async Task SendUserInvitationEmail(User user, string token, string tenantName)
    {
        var link = new Uri($"http://localhost:4200/auth/register?email={user.Email}&token={token}");
        var placeholders = new Dictionary<string, string>()
        {
            { "link", link.ToString() },
            { "tenantName", tenantName }
        };
        
        var content = await templateService.RenderTemplate(TemplateNames.InviteUser, placeholders);
        await emailSender.SendEmailAsync(user.Email!, "Complete registration", content, true);
    }
}