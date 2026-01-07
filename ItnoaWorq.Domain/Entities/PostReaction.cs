using ItnoaWorq.Domain.Entities.Common;
using ItnoaWorq.Domain.Enums;

namespace ItnoaWorq.Domain.Entities;

public class PostReaction : BaseEntity
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public ReactionType Type { get; set; }
}
