using ItnoaWorq.Domain.Entities.Common;

namespace ItnoaWorq.Domain.Entities;

public class ProfileSkill : BaseEntity
{
    public Guid ProfileId { get; set; }
    public Guid SkillId { get; set; }
    public int Proficiency { get; set; } // 1-5 scale
}
