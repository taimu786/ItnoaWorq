using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Common.DTOs;
using ItnoaWorq.Application.Features.Connections.Queries;
using ItnoaWorq.Domain.Entities;
using ItnoaWorq.Domain.Enums;
using MediatR;

public class GetConnectionSuggestionsHandler : IRequestHandler<GetConnectionSuggestionsQuery, List<ConnectionSuggestionDto>>
{
    private readonly IUserRepository _userRepo;
    private readonly IUnitOfWork _uow;

    public GetConnectionSuggestionsHandler(IUserRepository userRepo, IUnitOfWork uow)
    {
        _userRepo = userRepo;
        _uow = uow;
    }

    public async Task<List<ConnectionSuggestionDto>> Handle(GetConnectionSuggestionsQuery request, CancellationToken cancellationToken)
    {
        var allUsers = await _userRepo.GetAllAsync(cancellationToken);

        var connections = await _uow.Repository<Connection>()
            .FindAsync(c =>
                (c.FromUserId == request.UserId || c.ToUserId == request.UserId) &&
                c.Status == ConnectionStatus.Accepted,
                cancellationToken);

        var connectedIds = connections
            .Select(c => c.FromUserId == request.UserId ? c.ToUserId : c.FromUserId)
            .ToHashSet();

        var suggestions = allUsers
            .Where(u => u.Id != request.UserId && !connectedIds.Contains(u.Id))
            .OrderBy(_ => Guid.NewGuid())
            .Take(request.Limit)
            .Select(u => new ConnectionSuggestionDto
            {
                UserId = u.Id,
                FullName = u.FullName,
                Headline = u.PublicProfile?.Headline,
                ProfilePictureUrl = u.PublicProfile?.ProfilePictureUrl
            })
            .ToList();

        return suggestions;
    }
}
