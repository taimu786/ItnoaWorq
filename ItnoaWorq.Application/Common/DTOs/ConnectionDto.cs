namespace ItnoaWorq.Application.Common.DTOs;

public class ConnectionDto
{
    public Guid ConnectionId { get; set; }
    public Guid UserId { get; set; }
    public Guid ConnectedUserId { get; set; }
    public DateTime ConnectedAt { get; set; }
}
