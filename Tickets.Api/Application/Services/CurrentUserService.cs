using System.Security.Claims;
using Tickets.Api.Application.Interfaces;

namespace Tickets.Api.Application.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal? User =>
        _httpContextAccessor.HttpContext?.User;

    public Guid? UserId
    {
        get
        {
            var value = User?.FindFirstValue(ClaimTypes.NameIdentifier);

            return Guid.TryParse(value, out var userId)
                ? userId
                : null;
        }
    }

    public string? Email =>
        User?.FindFirstValue(ClaimTypes.Email);

    public string? FullName =>
        User?.FindFirstValue(ClaimTypes.Name);

    public string? Role =>
        User?.FindFirstValue(ClaimTypes.Role);

    public bool IsAuthenticated =>
        User?.Identity?.IsAuthenticated == true;

    public bool IsInRole(string role)
    {
        return User?.IsInRole(role) == true;
    }
}