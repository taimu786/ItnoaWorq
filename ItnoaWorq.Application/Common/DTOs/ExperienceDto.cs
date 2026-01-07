using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItnoaWorq.Application.Common.DTOs
{
    public class ExperienceDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = "";
        public string CompanyName { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }
        public bool IsCurrent { get; set; }
    }
}
