using ItnoaWorq.Application.Common.DTOs;
using ItnoaWorq.Domain.Entities;

namespace ItnoaWorq.Application.Common.Mapping;

public static class ProfileMapping
{
    public static ProfileDto ToDto(PublicProfile profile,
        IEnumerable<ProfileSkill> skills,
        IEnumerable<Skill> allSkills,
        IEnumerable<Education> education,
        IEnumerable<Experience> experience)
    {
        return new ProfileDto
        {
            Id = profile.Id,
            UserId = profile.UserId,
            Headline = profile.Headline,
            Summary = profile.Summary,
            Profession = profile.Profession,
            Location = profile.Location,
            Skills = (from ps in skills
                      join s in allSkills on ps.SkillId equals s.Id
                      select new SkillDto
                      {
                          Id = ps.Id,
                          Name = s.Name,
                          Proficiency = ps.Proficiency
                      }).ToList(),
            Education = education.Select(e => new EducationDto
            {
                Id = e.Id,
                School = e.School,
                Degree = e.Degree,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Description = e.Description
            }).ToList(),
            Experience = experience.Select(x => new ExperienceDto
            {
                Id = x.Id,
                Title = x.Title,
                CompanyName = x.CompanyName,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Description = x.Description,
                IsCurrent = x.IsCurrent
            }).ToList()
        };
    }
}
