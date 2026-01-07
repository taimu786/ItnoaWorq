using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItnoaWorq.Application.Common.DTOs
{
    public class ProfileDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Headline { get; set; } = "";
        public string Summary { get; set; } = "";
        public string Profession { get; set; } = "";
        public string Location { get; set; } = "";
        public List<SkillDto> Skills { get; set; } = new();
        public List<EducationDto> Education { get; set; } = new();
        public List<ExperienceDto> Experience { get; set; } = new();
    }
}
