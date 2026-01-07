using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Features.Jobs.Commands;
using ItnoaWorq.Domain.Entities;
using MediatR;

namespace ItnoaWorq.Application.Features.Jobs.Handlers;

public class EditJobHandler : IRequestHandler<EditJobCommand, Unit>
{
    private readonly IUnitOfWork _uow;

    public EditJobHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Unit> Handle(EditJobCommand request, CancellationToken cancellationToken)
    {
        var job = await _uow.Repository<Job>().GetByIdAsync(request.JobId, cancellationToken);
        if (job == null) throw new Exception("Job not found");

        job.Title = request.Title;
        job.Description = request.Description;
        job.Location = request.Location;

        _uow.Repository<Job>().Update(job);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
