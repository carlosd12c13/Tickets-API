using Microsoft.AspNetCore.Identity;
using Tickets.Application.DTOs.Auth;
using Tickets.Application.Interfaces;
using Tickets.Domain.Entities;

namespace Tickets.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IAuthTokenSettings _authTokenSettings;
    private readonly PasswordHasher<User> _passwordHasher;

    public AuthService(
        IUserRepository userRepository,
        IJwtTokenService jwtTokenService,
        IAuthTokenSettings authTokenSettings)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
        _authTokenSettings = authTokenSettings;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetActiveByEmailAsync(request.Email);

        if (user is null)
            return null;

        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password
        );

        if (result == PasswordVerificationResult.Failed)
            return null;

        var expiresAt = DateTime.UtcNow.AddMinutes(_authTokenSettings.ExpirationMinutes);
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
