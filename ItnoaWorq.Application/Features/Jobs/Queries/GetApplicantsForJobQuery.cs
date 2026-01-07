using ItnoaWorq.Application.Common.DTOs;
using MediatR;

namespace ItnoaWorq.Application.Features.Jobs.Queries;

public record GetApplicantsForJobQuery(Guid JobId, int Page = 1, int PageSize = 10, string? Status = null)
    : IRequest<List<JobApplicationDto>>;
