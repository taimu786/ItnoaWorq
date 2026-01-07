using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItnoaWorq.Application.Common.DTOs
{
    public class EducationDto
    {
        public Guid Id { get; set; }
        public string School { get; set; } = "";
        public string Degree { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }
    }
}
