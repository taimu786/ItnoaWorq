using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItnoaWorq.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TenantsController : ControllerBase
{
    private readonly IUnitOfWork _uow;

    public TenantsController(IUnitOfWork uow) => _uow = uow;

    public record CreateTenantDto(string Name, string Slug, Guid PlanId);

    [HttpPost]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Create([FromBody] CreateTenantDto dto, CancellationToken ct)
    {
        var tenant = new Tenant
        {
            Name = dto.Name,
            Slug = dto.Slug,
            CurrentPlanId = dto.PlanId
        };

        var repo = _uow.Repository<Tenant>();
        await repo.AddAsync(tenant, ct);
        await _uow.SaveChangesAsync(ct);

        return Ok(new { id = tenant.Id });
    }
}
