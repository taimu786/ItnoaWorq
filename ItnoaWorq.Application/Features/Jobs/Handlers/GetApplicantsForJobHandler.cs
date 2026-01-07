using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Common.DTOs;
using ItnoaWorq.Application.Features.Jobs.Queries;
using ItnoaWorq.Domain.Entities;
using MediatR;

namespace ItnoaWorq.Application.Features.Jobs.Handlers;

public class GetApplicantsForJobHandler : IRequestHandler<GetApplicantsForJobQuery, List<JobApplicationDto>>
{
    private readonly IUnitOfWork _uow;
    public GetApplicantsForJobHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<List<JobApplicationDto>> Handle(GetApplicantsForJobQuery request, CancellationToken cancellationToken)
    {
        var apps = await _uow.Repository<JobApplication>()
            .FindAsync(a => a.JobId == request.JobId &&
                           (request.Status == null || a.Status.ToString() == request.Status),
                        cancellationToken);

        return apps
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(a => new JobApplicationDto
            {
                Id = a.Id,
                JobId = a.JobId,
                UserId = a.CandidateUserId,
                AppliedAt = a.AppliedAt,
                Status = a.Status
            })
            .ToList();
    }
}
