﻿using ErrorOr;
using Microsoft.AspNetCore.Identity;
using RoboTasker.Application.Common.Abstractions;
using RoboTasker.Domain.Tenants;
using RoboTasker.Infrastructure.Authentication.Services;

namespace RoboTasker.Infrastructure.Authentication.RefreshToken;

public class RefreshTokenHandler(TokenService tokenService, UserManager<User> userManager) : ICommandHandler<RefreshTokenCommand, AccessTokenResponse>
{
    public async Task<ErrorOr<AccessTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var email = tokenService.GetEmailFromExpiredToken(request.Token);
        if (string.IsNullOrEmpty(email))
        {
            return Error.Unauthorized();
        }
        
        var user = await userManager.FindByEmailAsync(email);

        if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiresAt < DateTime.UtcNow)
        {
            return Error.Unauthorized();
        }

        var newAccessToken = tokenService.GenerateToken(user);
        var refreshToken = tokenService.GenerateRefreshToken();
        
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiresAt = DateTimeOffset.UtcNow.AddDays(2);
        
        await userManager.UpdateAsync(user);
        
        newAccessToken.RefreshToken = refreshToken;
        
        return newAccessToken;
    }
}