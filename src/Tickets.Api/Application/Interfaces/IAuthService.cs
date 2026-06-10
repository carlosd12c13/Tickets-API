using Tickets.Api.Application.DTOs.Auth;

namespace Tickets.Api.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
}