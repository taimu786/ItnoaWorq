using ItnoaWorq.Domain.Entities.Common;

namespace ItnoaWorq.Domain.Entities;

public class Experience : BaseEntity
{
    public Guid ProfileId { get; set; }
    public string Title { get; set; } = "";
    public string CompanyName { get; set; } = "";
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; }
    public bool IsCurrent { get; set; }
}
