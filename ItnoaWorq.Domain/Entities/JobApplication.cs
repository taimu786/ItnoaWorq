using ItnoaWorq.Domain.Entities.Common;
using ItnoaWorq.Domain.Enums;

namespace ItnoaWorq.Domain.Entities;

public class JobApplication : BaseEntity
{
    public Guid JobId { get; set; }
    public Guid CandidateUserId { get; set; } // Identity user id
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied;
    public string? CvUrl { get; set; }
    public string? CoverLetter { get; set; }
    public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
}
