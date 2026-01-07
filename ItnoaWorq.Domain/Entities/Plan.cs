using ItnoaWorq.Domain.Entities.Common;
using ItnoaWorq.Domain.Enums;

namespace ItnoaWorq.Domain.Entities;

public class Plan : BaseEntity
{
    public string Name { get; set; } = default!;
    public PlanCategory Category { get; set; }
    public PlanType Type { get; set; }
    public bool IsActive { get; set; } = true;
}
