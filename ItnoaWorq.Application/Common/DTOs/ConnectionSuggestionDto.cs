namespace ItnoaWorq.Application.Common.DTOs;

public class ConnectionSuggestionDto
{
    public Guid UserId { get; set; }
    public string? FullName { get; set; }
    public string? Headline { get; set; }
    public string? ProfilePictureUrl { get; set; }
}
