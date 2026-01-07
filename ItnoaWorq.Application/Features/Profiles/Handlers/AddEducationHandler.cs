using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Features.Profiles.Commands;
using ItnoaWorq.Domain.Entities;
using MediatR;

namespace ItnoaWorq.Application.Features.Profiles.Handlers;

public class AddEducationHandler : IRequestHandler<ApplyForJobCommand, Unit>
{
    private readonly IUnitOfWork _uow;

    public AddEducationHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Unit> Handle(ApplyForJobCommand request, CancellationToken cancellationToken)
    {
        var profileRepo = _uow.Repository<PublicProfile>();
        var profile = (await profileRepo.FindAsync(p => p.UserId == request.UserId, cancellationToken)).FirstOrDefault();
        if (profile == null) throw new Exception("Profile not found");

        var education = new Education
        {
            ProfileId = profile.Id,
            School = request.Education.School,
            Degree = request.Education.Degree,
            StartDate = request.Education.StartDate,
            EndDate = request.Education.EndDate,
            Description = request.Education.Description
        };

        await _uow.Repository<Education>().AddAsync(education, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
