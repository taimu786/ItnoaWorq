namespace ItnoaWorq.Application.Common.DTOs
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid AuthorUserId { get; set; }
        public string Content { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}