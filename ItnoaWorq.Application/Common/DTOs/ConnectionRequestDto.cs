namespace ItnoaWorq.Application.Common.DTOs;

public class ConnectionRequestDto
{
    public Guid RequestId { get; set; }
    public Guid FromUserId { get; set; }
    public DateTime RequestedAt { get; set; }
}
