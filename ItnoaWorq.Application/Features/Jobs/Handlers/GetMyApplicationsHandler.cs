using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Common.DTOs;
using ItnoaWorq.Application.Features.Jobs.Queries;
using ItnoaWorq.Domain.Entities;
using MediatR;

namespace ItnoaWorq.Application.Features.Jobs.Handlers;

public class GetMyApplicationsHandler : IRequestHandler<GetMyApplicationsQuery, List<JobApplicationDto>>
{
    private readonly IUnitOfWork _uow;

    public GetMyApplicationsHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<List<JobApplicationDto>> Handle(GetMyApplicationsQuery request, CancellationToken cancellationToken)
    {
        var apps = await _uow.Repository<JobApplication>()
            .FindAsync(a => a.CandidateUserId == request.UserId, cancellationToken);

        return apps.Select(a => new JobApplicationDto
        {
            Id = a.Id,
            JobId = a.JobId,
            UserId = a.CandidateUserId,
            AppliedAt = a.AppliedAt,
            Status = a.Status
        }).ToList();
    }
}
