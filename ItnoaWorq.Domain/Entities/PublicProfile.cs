using ItnoaWorq.Domain.Entities.Common;

namespace ItnoaWorq.Domain.Entities;

public class PublicProfile : BaseEntity
{
    public Guid UserId { get; set; }      // Identity user
    public string Headline { get; set; } = "";
    public string Summary { get; set; } = "";
    public string Profession { get; set; } = "";
    public string Location { get; set; } = "";
    public string? ProfilePictureUrl { get; set; }
}
