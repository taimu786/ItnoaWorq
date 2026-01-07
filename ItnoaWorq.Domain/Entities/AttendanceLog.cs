using ItnoaWorq.Domain.Entities.Common;

namespace ItnoaWorq.Domain.Entities;

public class AttendanceLog : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
}
