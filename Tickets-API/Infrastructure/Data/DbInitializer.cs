using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tickets_API.Domain.Entities;
using Tickets_API.Domain.Enums;

namespace Tickets_API.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.Users.AnyAsync())
            return;

        var passwordHasher = new PasswordHasher<User>();

        var admin = new User
        {
            Id = Guid.NewGuid(),
            FullName = "Admin User",
            Email = "admin@test.com",
            Role = UserRole.Admin,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin123!");

        var agent = new User
        {
            Id = Guid.NewGuid(),
            FullName = "Support Agent",
            Email = "agent@test.com",
            Role = UserRole.Agent,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        agent.PasswordHash = passwordHasher.HashPassword(agent, "Agent123!");

        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = "Regular User",
            Email = "user@test.com",
            Role = UserRole.User,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        user.PasswordHash = passwordHasher.HashPassword(user, "User123!");

        context.Users.AddRange(admin, agent, user);

        await context.SaveChangesAsync();
    }
}