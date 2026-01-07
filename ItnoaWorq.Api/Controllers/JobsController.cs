using ItnoaWorq.Application.Common.DTOs;
using ItnoaWorq.Application.Features.Jobs.Commands;
using ItnoaWorq.Application.Features.Jobs.Queries;
using ItnoaWorq.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItnoaWorq.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _http;

    public JobsController(IMediator mediator, IHttpContextAccessor http)
    {
        _mediator = mediator;
        _http = http;
    }

    private Guid GetUserId() =>
        Guid.Parse(_http.HttpContext!.User.Claims.First(c => c.Type == "uid").Value);

    private Guid GetTenantId() =>
        Guid.Parse(_http.HttpContext!.Request.Headers["X-Tenant"].ToString());

    // Candidate APIs
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<JobDto>>> GetAllJobs() =>
        await _mediator.Send(new GetAllJobsQuery());

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<JobDto?>> GetJobById(Guid id) =>
        await _mediator.Send(new GetJobByIdQuery(id));

    [HttpPost("{id}/apply")]
    [Authorize(Roles = "Public,Employee")]
    public async Task<IActionResult> Apply(Guid id)
    {
        await _mediator.Send(new ApplyForJobCommand(GetUserId(), id));
        return Ok(new { message = "Applied successfully" });
    }

    // Employer APIs
    [HttpPost("post")]
    [Authorize(Roles = "HR,Admin")]
    public async Task<IActionResult> PostJob([FromBody] PostJobCommand cmd)
    {
        var jobId = await _mediator.Send(cmd with { TenantId = GetTenantId() });
        return Ok(new { id = jobId });
    }

    [HttpGet("my-jobs")]
    [Authorize(Roles = "HR,Admin")]
    public async Task<ActionResult<List<JobDto>>> GetMyJobs([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
    {
        return await _mediator.Send(new GetMyJobsQuery(GetTenantId(), page, pageSize, search));
    }

    [HttpGet("{id}/applicants")]
    [Authorize(Roles = "HR,Admin")]
    public async Task<ActionResult<List<JobApplicationDto>>> GetApplicants(Guid id, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? status = null)
    {
        return await _mediator.Send(new GetApplicantsForJobQuery(id, page, pageSize, status));
    }

    [HttpPut("applications/{id}/status")]
    [Authorize(Roles = "HR,Admin")]
    public async Task<IActionResult> UpdateApplicationStatus(Guid id, [FromBody] ApplicationStatus status)
    {
        await _mediator.Send(new UpdateApplicationStatusCommand(id, status));
        return Ok(new { message = "Status updated" });
    }

    [HttpGet("applications")]
    [Authorize(Roles = "Public,Employee")]
    public async Task<ActionResult<List<JobApplicationDto>>> GetMyApplications()
    {
        var apps = await _mediator.Send(new GetMyApplicationsQuery(GetUserId()));
        return Ok(apps);
    }


    [HttpPut("{id}")]
    [Authorize(Roles = "HR,Admin")]
    public async Task<IActionResult> EditJob(Guid id, [FromBody] EditJobCommand cmd)
    {
        await _mediator.Send(cmd with { JobId = id });
        return Ok(new { message = "Job updated successfully" });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "HR,Admin")]
    public async Task<IActionResult> DeleteJob(Guid id)
    {
        await _mediator.Send(new DeleteJobCommand(id));
        return Ok(new { message = "Job deleted successfully" });
    }
}
