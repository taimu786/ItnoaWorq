using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Features.Jobs.Commands;
using ItnoaWorq.Domain.Entities;
using ItnoaWorq.Domain.Enums;
using MediatR;

namespace ItnoaWorq.Application.Features.Jobs.Handlers;

public class ApplyForJobHandler : IRequestHandler<ApplyForJobCommand, Unit>
{
    private readonly IUnitOfWork _uow;

    public ApplyForJobHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Unit> Handle(ApplyForJobCommand request, CancellationToken cancellationToken)
    {
        var job = await _uow.Repository<Job>().GetByIdAsync(request.JobId, cancellationToken);
        if (job == null) throw new Exception("Job not found");

        var existing = (await _uow.Repository<JobApplication>()
            .FindAsync(a => a.JobId == request.JobId && a.CandidateUserId == request.UserId, cancellationToken))
            .FirstOrDefault();

        if (existing != null)
            throw new Exception("Already applied for this job");

        var application = new JobApplication
        {
            JobId = request.JobId,
            CandidateUserId = request.UserId,
            AppliedAt = DateTime.UtcNow,
            Status = ApplicationStatus.Applied
        };

        await _uow.Repository<JobApplication>().AddAsync(application, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
