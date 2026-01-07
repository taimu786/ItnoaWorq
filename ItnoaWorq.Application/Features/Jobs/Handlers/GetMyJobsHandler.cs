using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Common.DTOs;
using ItnoaWorq.Application.Features.Jobs.Queries;
using ItnoaWorq.Domain.Entities;
using MediatR;

namespace ItnoaWorq.Application.Features.Jobs.Handlers;

public class GetMyJobsHandler : IRequestHandler<GetMyJobsQuery, List<JobDto>>
{
    private readonly IUnitOfWork _uow;
    public GetMyJobsHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<List<JobDto>> Handle(GetMyJobsQuery request, CancellationToken cancellationToken)
    {
        var jobs = await _uow.Repository<Job>()
            .FindAsync(j => j.TenantId == request.TenantId &&
                           (string.IsNullOrEmpty(request.Search) || j.Title.Contains(request.Search)),
                        cancellationToken);

        return jobs
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(j => new JobDto
            {
                Id = j.Id,
                Title = j.Title,
                Description = j.Description,
                Location = j.Location,
                TenantId = j.TenantId,
                CreatedAt = j.CreatedAt
            })
            .ToList();
    }
}
