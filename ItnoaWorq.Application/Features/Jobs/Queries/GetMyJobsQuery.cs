using ItnoaWorq.Application.Common.DTOs;
using MediatR;

namespace ItnoaWorq.Application.Features.Jobs.Queries;

public record GetMyJobsQuery(Guid TenantId, int Page = 1, int PageSize = 10, string? Search = null)
    : IRequest<List<JobDto>>;
