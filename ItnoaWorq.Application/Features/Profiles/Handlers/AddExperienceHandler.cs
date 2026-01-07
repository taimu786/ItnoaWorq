using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Features.Profiles.Commands;
using ItnoaWorq.Domain.Entities;
using MediatR;

namespace ItnoaWorq.Application.Features.Profiles.Handlers;

public class AddExperienceHandler : IRequestHandler<AddExperienceCommand, Unit>
{
    private readonly IUnitOfWork _uow;

    public AddExperienceHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Unit> Handle(AddExperienceCommand request, CancellationToken cancellationToken)
    {
        var profileRepo = _uow.Repository<PublicProfile>();
        var profile = (await profileRepo.FindAsync(p => p.UserId == request.UserId, cancellationToken)).FirstOrDefault();
        if (profile == null) throw new Exception("Profile not found");

        var experience = new Experience
        {
            ProfileId = profile.Id,
            Title = request.Experience.Title,
            CompanyName = request.Experience.CompanyName,
            StartDate = request.Experience.StartDate,
            EndDate = request.Experience.EndDate,
            Description = request.Experience.Description,
            IsCurrent = request.Experience.IsCurrent
        };

        await _uow.Repository<Experience>().AddAsync(experience, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
