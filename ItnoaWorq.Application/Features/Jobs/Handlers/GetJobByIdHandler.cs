using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Common.DTOs;
using ItnoaWorq.Application.Features.Jobs.Queries;
using ItnoaWorq.Domain.Entities;
using MediatR;

namespace ItnoaWorq.Application.Features.Jobs.Handlers;

public class GetJobByIdHandler : IRequestHandler<GetJobByIdQuery, JobDto?>
{
    private readonly IUnitOfWork _uow;

    public GetJobByIdHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<JobDto?> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
    {
        var job = await _uow.Repository<Job>().GetByIdAsync(request.JobId, cancellationToken);
        if (job == null) return null;

        return new JobDto
        {
            Id = job.Id,
            Title = job.Title,
            Description = job.Description,
            Location = job.Location,
            TenantId = job.TenantId,
            CreatedAt = job.CreatedAt
        };
    }
}
