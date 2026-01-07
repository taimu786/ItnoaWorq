namespace ItnoaWorq.Application.Common.DTOs;

public class JobDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Location { get; set; } = "";
    public Guid? TenantId { get; set; }
    public DateTime CreatedAt { get; set; }
}