using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Features.Profiles.Commands;
using ItnoaWorq.Domain.Entities;
using MediatR;

namespace ItnoaWorq.Application.Profiles.Handlers;

public class UpdateMyProfileHandler : IRequestHandler<UpdateMyProfileCommand, Unit>
{
    private readonly IUnitOfWork _uow;

    public UpdateMyProfileHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Unit> Handle(UpdateMyProfileCommand request, CancellationToken cancellationToken)
    {
        var repo = _uow.Repository<PublicProfile>();
        var profile = (await repo.FindAsync(p => p.UserId == request.UserId)).FirstOrDefault();
        if (profile == null) throw new Exception("Profile not found");

        profile.Headline = request.Profile.Headline;
        profile.Summary = request.Profile.Summary;
        profile.Profession = request.Profile.Profession;
        profile.Location = request.Profile.Location;
        profile.UpdatedAt = DateTime.UtcNow;

        repo.Update(profile);
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
