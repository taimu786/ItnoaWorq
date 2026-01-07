using ItnoaWorq.Domain.Enums;

namespace ItnoaWorq.Application.Common.DTOs
{
    public class ReactionDto
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public ReactionType Type { get; set; } = ReactionType.Like;
    }
}