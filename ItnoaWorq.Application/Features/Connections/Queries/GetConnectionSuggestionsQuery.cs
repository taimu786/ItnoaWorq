using ItnoaWorq.Application.Common.DTOs;
using MediatR;

public record GetConnectionSuggestionsQuery(Guid UserId, int Limit = 10) : IRequest<List<ConnectionSuggestionDto>>;