using ItnoaWorq.Domain.Entities.Common;
using ItnoaWorq.Domain.Enums;

namespace ItnoaWorq.Domain.Entities;

public class PerformanceReview : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public string ReviewPeriod { get; set; } = default!; // e.g., "2025-Q1"
    public ReviewStatus Status { get; set; } = ReviewStatus.Draft;
    public string? Feedback { get; set; }
    public int? Rating { get; set; }
}
