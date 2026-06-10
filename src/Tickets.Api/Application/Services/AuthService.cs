using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Tickets.Api.Application.DTOs.Auth;
using Tickets.Api.Application.Interfaces;
using Tickets.Api.Domain.Entities;
using Tickets.Api.Infrastructure.Auth;
using Tickets.Api.Infrastructure.Data;

namespace Tickets.Api.Application.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly JwtOptions _jwtOptions;
    private readonly PasswordHasher<User> _passwordHasher;

    public AuthService(
        AppDbContext context,
        IJwtTokenService jwtTokenService,
        IOptions<JwtOptions> jwtOptions)
    {
        _context = context;
        _jwtTokenService = jwtTokenService;
        _jwtOptions = jwtOptions.Value;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == request.Email && x.IsActive);

        if (user is null)
            return null;

        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password
        );

        if (result == PasswordVerificationResult.Failed)
            return null;

        var expiresAt = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes);
        var token = _jwtTokenService.CreateToken(user, expiresAt);

        return new LoginResponse
        {
            AccessToken = token,
            ExpiresAt = expiresAt,
            Email = user.Email,
            Role = user.Role.ToString()
        };
    }
}