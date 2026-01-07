using ItnoaWorq.Domain.Entities.Common;
using ItnoaWorq.Domain.Enums;

namespace ItnoaWorq.Domain.Entities;

public class Employee : BaseEntity
{
    public Guid UserId { get; set; }                // personal account
    public Guid TenantId { get; set; }              // company they belong to
    public string EmployeeNo { get; set; } = "";    // optional unique code
    public string? Designation { get; set; }
    public EmploymentStatus EmploymentStatus { get; set; } = EmploymentStatus.Active;
    public DateTime JoinDate { get; set; } = DateTime.UtcNow;
    public DateTime? ExitDate { get; set; }

    // Visibility:
    // If Tenant.Plan.Type = Free → Employee shows under company profile, experience visible
    // If Tenant.Plan.Type = Paid → Employee unlocks internal HR features (attendance, payroll, etc.)
}
