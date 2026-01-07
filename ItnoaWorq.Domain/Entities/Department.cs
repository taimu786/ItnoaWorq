using ItnoaWorq.Domain.Entities.Common;

namespace ItnoaWorq.Domain.Entities;

public class Department : BaseEntity
{
    public string Name { get; set; } = default!;
    public Guid? ParentDeptId { get; set; }
}
