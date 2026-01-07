using ItnoaWorq.Domain.Entities.Common;
using ItnoaWorq.Domain.Enums;

namespace ItnoaWorq.Domain.Entities;

public class Payroll : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public PayrollStatus Status { get; set; } = PayrollStatus.Open;
    public decimal NetPay { get; set; }
}
