using Tickets.Api.Domain.Enums;

namespace Tickets.Api.Domain.Entities;

public class TicketStatusHistory
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid TicketId { get; set; }

    public Ticket? Ticket { get; set; }

    public TicketStatus OldStatus { get; set; }

    public TicketStatus NewStatus { get; set; }

    public Guid ChangedByUserId { get; set; }

    public User? ChangedByUser { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
}