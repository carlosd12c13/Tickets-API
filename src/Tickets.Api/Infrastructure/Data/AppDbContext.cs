using Microsoft.EntityFrameworkCore;
using Tickets.Api.Domain.Entities;

namespace Tickets.Api.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<TicketComment> TicketComments => Set<TicketComment>();
    public DbSet<TicketStatusHistory> TicketStatusHistory => Set<TicketStatusHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureUsers(modelBuilder);
        ConfigureTickets(modelBuilder);
        ConfigureTicketComments(modelBuilder);
        ConfigureTicketStatusHistory(modelBuilder);
    }

    private static void ConfigureUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(x => x.PasswordHash)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(x => x.Role)
                .IsRequired();

            entity.Property(x => x.IsActive)
                .IsRequired();

            entity.Property(x => x.CreatedAt)
                .IsRequired();

            entity.HasIndex(x => x.Email)
                .IsUnique();
        });
    }

    private static void ConfigureTickets(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.ToTable("Tickets");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1000);

            entity.Property(x => x.Status)
                .IsRequired();

            entity.Property(x => x.CreatedAt)
                .IsRequired();

            entity.HasOne(x => x.CreatedByUser)
                .WithMany(x => x.CreatedTickets)
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.AssignedToUser)
                .WithMany(x => x.AssignedTickets)
                .HasForeignKey(x => x.AssignedToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(x => x.Status);
            entity.HasIndex(x => x.CreatedAt);
            entity.HasIndex(x => x.CreatedByUserId);
            entity.HasIndex(x => x.AssignedToUserId);
        });
    }

    private static void ConfigureTicketComments(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TicketComment>(entity =>
        {
            entity.ToTable("TicketComments");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Message)
                .IsRequired()
                .HasMaxLength(1000);

            entity.Property(x => x.CreatedAt)
                .IsRequired();

            entity.HasOne(x => x.Ticket)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(x => x.TicketId);
            entity.HasIndex(x => x.UserId);
        });
    }

    private static void ConfigureTicketStatusHistory(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TicketStatusHistory>(entity =>
        {
            entity.ToTable("TicketStatusHistory");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.OldStatus)
                .IsRequired();

            entity.Property(x => x.NewStatus)
                .IsRequired();

            entity.Property(x => x.ChangedAt)
                .IsRequired();

            entity.HasOne(x => x.Ticket)
                .WithMany(x => x.StatusHistory)
                .HasForeignKey(x => x.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.ChangedByUser)
                .WithMany()
                .HasForeignKey(x => x.ChangedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(x => x.TicketId);
            entity.HasIndex(x => x.ChangedByUserId);
            entity.HasIndex(x => x.ChangedAt);
        });
    }
}