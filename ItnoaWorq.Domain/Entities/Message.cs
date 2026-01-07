using ItnoaWorq.Domain.Entities.Common;

namespace ItnoaWorq.Domain.Entities;

public class Message : BaseEntity
{
    public Guid SenderId { get; set; }      // Identity user id
    public Guid ReceiverId { get; set; }    // Identity user id
    public string Content { get; set; } = default!;
    public bool IsRead { get; set; } = false;
}
