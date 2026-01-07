using ItnoaWorq.Domain.Entities.Common;

namespace ItnoaWorq.Domain.Entities;

public class Notification : BaseEntity
{
    public Guid UserId { get; set; } // Identity user id
    public string Message { get; set; } = default!;
    public bool IsRead { get; set; } = false;
}
