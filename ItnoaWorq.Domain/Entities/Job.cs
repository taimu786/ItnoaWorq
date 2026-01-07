using ItnoaWorq.Domain.Entities.Common;
using ItnoaWorq.Domain.Enums;

namespace ItnoaWorq.Domain.Entities;

public class Job : BaseEntity
{
    public string Title { get; set; } = default!;
    public string? Department { get; set; }
    public JobType Type { get; set; } = JobType.FullTime;
    public string Location { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool IsActive { get; set; } = true;
    public DateTime PostedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiresAt { get; set; }
}
