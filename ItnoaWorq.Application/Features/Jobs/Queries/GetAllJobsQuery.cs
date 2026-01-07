using ItnoaWorq.Application.Common.DTOs;
using MediatR;

namespace ItnoaWorq.Application.Features.Jobs.Queries;

public record GetAllJobsQuery() : IRequest<List<JobDto>>;
