using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Common.DTOs;
using ItnoaWorq.Application.Features.Jobs.Queries;
using ItnoaWorq.Domain.Entities;
using MediatR;

namespace ItnoaWorq.Application.Features.Jobs.Handlers;

public class GetAllJobsHandler : IRequestHandler<GetAllJobsQuery, List<JobDto>>
{
    private readonly IUnitOfWork _uow;

    public GetAllJobsHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<List<JobDto>> Handle(GetAllJobsQuery request, CancellationToken cancellationToken)
    {
        var jobs = await _uow.Repository<Job>().GetAllAsync(cancellationToken);

        return jobs.Select(j => new JobDto
        {
            Id = j.Id,
            Title = j.Title,
            Description = j.Description,
            Location = j.Location,
            TenantId = j.TenantId,
            CreatedAt = j.CreatedAt
        }).ToList();
    }
}
