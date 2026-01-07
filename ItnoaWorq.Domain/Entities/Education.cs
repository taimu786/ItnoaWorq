using ItnoaWorq.Domain.Entities.Common;

namespace ItnoaWorq.Domain.Entities;

public class Education : BaseEntity
{
    public Guid ProfileId { get; set; }
    public string School { get; set; } = "";
    public string Degree { get; set; } = "";
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; }
}
