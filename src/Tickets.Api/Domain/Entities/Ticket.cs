using Tickets.Api.Domain.Enums;
using Tickets.Api.Exceptions;

namespace Tickets.Api.Domain.Entities;

public class Ticket
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public TicketStatus Status { get; private set; } = TicketStatus.Open;

    public Guid CreatedByUserId { get; set; }

    public User? CreatedByUser { get; set; }

    public Guid? AssignedToUserId { get; private set; }

    public User? AssignedToUser { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ClosedAt { get; private set; }

    public ICollection<TicketComment> Comments { get; set; } = new List<TicketComment>();

    public ICollection<TicketStatusHistory> StatusHistory { get; set; } = new List<TicketStatusHistory>();

    public void AssignTo(Guid agentUserId)
    {
        if (Status == TicketStatus.Closed)
            throw new DomainException("Closed tickets cannot be assigned.");

        AssignedToUserId = agentUserId;

        if (Status == TicketStatus.Open)
            Status = TicketStatus.InProgress;
    }

    public void ChangeStatus(TicketStatus newStatus)
    {
        if (Status == TicketStatus.Closed)
            throw new DomainException("Closed tickets cannot be modified.");

        if (newStatus == TicketStatus.Open && Status != TicketStatus.Open)
            throw new DomainException("A ticket cannot return to Open status.");

        Status = newStatus;

        if (newStatus == TicketStatus.Closed)
            ClosedAt = DateTime.UtcNow;
    }

    public void AddComment(Guid userId, string message)
    {
        if (Status == TicketStatus.Closed)
            throw new DomainException("Cannot add comments to a closed ticket.");

        if (string.IsNullOrWhiteSpace(message))
            throw new DomainException("Comment message cannot be empty.");

        Comments.Add(new TicketComment
        {
            TicketId = Id,
            UserId = userId,
            Message = message
        });
    }
}