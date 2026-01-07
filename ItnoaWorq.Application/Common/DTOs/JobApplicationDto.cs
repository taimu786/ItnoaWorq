using ItnoaWorq.Domain.Enums;

namespace ItnoaWorq.Application.Common.DTOs;

public class JobApplicationDto
{
    public Guid Id { get; set; }
    public Guid JobId { get; set; }
    public Guid UserId { get; set; }
    public DateTime AppliedAt { get; set; }
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied;
}