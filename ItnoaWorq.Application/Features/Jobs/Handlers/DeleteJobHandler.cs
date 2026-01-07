using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Features.Jobs.Commands;
using ItnoaWorq.Domain.Entities;
using MediatR;

namespace ItnoaWorq.Application.Features.Jobs.Handlers;

public class DeleteJobHandler : IRequestHandler<DeleteJobCommand, Unit>
{
    private readonly IUnitOfWork _uow;

    public DeleteJobHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Unit> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
    {
        var job = await _uow.Repository<Job>().GetByIdAsync(request.JobId, cancellationToken);
        if (job == null) throw new Exception("Job not found");

        await _uow.Repository<Job>().DeleteAsync(job, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
