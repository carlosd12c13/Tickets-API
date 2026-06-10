using Tickets.Application.DTOs.Auth;

namespace Tickets.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
}