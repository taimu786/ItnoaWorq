
namespace ItnoaWorq.Application.Common.DTOs
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public Guid AuthorUserId { get; set; }   // or TenantId if business
        public string Content { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public List<CommentDto> Comments { get; set; } = new();
        public List<ReactionDto> Reactions { get; set; } = new();
    }
}
