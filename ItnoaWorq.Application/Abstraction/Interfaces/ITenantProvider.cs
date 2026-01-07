namespace ItnoaWorq.Application.Abstraction.Interfaces;

public interface ITenantProvider
{
    Guid? CurrentTenantId { get; }
}
