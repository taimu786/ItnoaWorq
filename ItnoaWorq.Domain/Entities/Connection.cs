using ItnoaWorq.Domain.Entities.Common;
using ItnoaWorq.Domain.Enums;
using System.Data;

namespace ItnoaWorq.Domain.Entities;

public class Connection : BaseEntity
{
    public Guid FromUserId { get; set; }   // requester
    public Guid ToUserId { get; set; }    // connecting with another person
    public Guid ToTenantId { get; set; }  // connecting with a company
    public bool IsAccepted { get; set; }   // for user ↔ user connections
    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    public DateTime? AcceptedAt { get; set; }
    public ConnectionStatus Status { get; set; } = ConnectionStatus.Pending;
}
