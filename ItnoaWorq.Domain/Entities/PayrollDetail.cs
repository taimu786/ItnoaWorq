using ItnoaWorq.Domain.Entities.Common;
using ItnoaWorq.Domain.Enums;

namespace ItnoaWorq.Domain.Entities;

public class PayrollDetail : BaseEntity
{
    public Guid PayrollId { get; set; }
    public PayComponentType Type { get; set; }
    public string ComponentName { get; set; } = default!;
    public decimal Amount { get; set; }
}
