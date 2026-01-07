using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Features.Jobs.Commands;
using ItnoaWorq.Domain.Entities;
using MediatR;

namespace ItnoaWorq.Application.Features.Jobs.Handlers;

public class UpdateApplicationStatusHandler : IRequestHandler<UpdateApplicationStatusCommand, Unit>
{
    private readonly IUnitOfWork _uow;
    public UpdateApplicationStatusHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Unit> Handle(UpdateApplicationStatusCommand request, CancellationToken cancellationToken)
    {
        var app = await _uow.Repository<JobApplication>().GetByIdAsync(request.ApplicationId, cancellationToken);
        if (app == null) throw new Exception("Application not found");

        app.Status = request.Status;

        _uow.Repository<JobApplication>().Update(app);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
