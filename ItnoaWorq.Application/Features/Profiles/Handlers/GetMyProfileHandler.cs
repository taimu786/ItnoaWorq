using ItnoaWorq.Application.Common.DTOs;
using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Features.Profiles.Queries;
using ItnoaWorq.Domain.Entities;
using MediatR;
using ItnoaWorq.Application.Common.Mapping;

namespace ItnoaWorq.Application.Profiles.Handlers;

public class GetMyProfileHandler : IRequestHandler<GetJobByIdQuery, ProfileDto>
{
    private readonly IUnitOfWork _uow;

    public GetMyProfileHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<ProfileDto> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
    {
        var repo = _uow.Repository<PublicProfile>();
        var profile = (await repo.FindAsync(p => p.UserId == request.UserId)).FirstOrDefault();

        if (profile == null)
        {
            profile = new PublicProfile { UserId = request.UserId };
            await repo.AddAsync(profile);
            await _uow.SaveChangesAsync(cancellationToken);
        }

        var skills = await _uow.Repository<ProfileSkill>().FindAsync(x => x.ProfileId == profile.Id);
        var allSkills = await _uow.Repository<Skill>().GetAllAsync();
        var education = await _uow.Repository<Education>().FindAsync(x => x.ProfileId == profile.Id);
        var experience = await _uow.Repository<Experience>().FindAsync(x => x.ProfileId == profile.Id);

        return ProfileMapping.ToDto(profile, skills, allSkills, education, experience);
    }
}
