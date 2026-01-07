using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItnoaWorq.Application.Common.DTOs
{
    public class SkillDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public int Proficiency { get; set; }
    }
}
