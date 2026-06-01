using Tickets_API.Application.DTOs.Auth;

namespace Tickets_API.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
}