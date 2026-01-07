using ItnoaWorq.Domain.Entities.Common;

namespace ItnoaWorq.Domain.Entities;

public class Post : BaseEntity
{
    public Guid AuthorUserId { get; set; }
    public Guid? TenantId { get; set; }   // if posted by a company
    public string Body { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
