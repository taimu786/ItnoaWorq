using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Features.Jobs.Commands;
using ItnoaWorq.Domain.Entities;
using MediatR;

namespace ItnoaWorq.Application.Features.Jobs.Handlers;

public class PostJobHandler : IRequestHandler<PostJobCommand, Guid>
{
    private readonly IUnitOfWork _uow;
    public PostJobHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Guid> Handle(PostJobCommand request, CancellationToken cancellationToken)
    {
        var job = new Job
        {
            TenantId = request.TenantId,
            Title = request.Title,
            Description = request.Description,
            Location = request.Location,
            CreatedAt = DateTime.UtcNow
        };

        await _uow.Repository<Job>().AddAsync(job, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return job.Id;
    }
}
