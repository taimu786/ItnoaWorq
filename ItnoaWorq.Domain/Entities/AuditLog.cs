using ItnoaWorq.Domain.Entities.Common;

namespace ItnoaWorq.Domain.Entities;

public class AuditLog : BaseEntity
{
    public Guid UserId { get; set; }            // Actor - Identity user id
    public string Action { get; set; } = default!;
    public string EntityName { get; set; } = default!;
    public Guid EntityId { get; set; }
    public DateTime ActionTime { get; set; } = DateTime.UtcNow;
}
