using ItnoaWorq.Application.Abstraction.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ItnoaWorq.Infrastructure.Tenancy;

public class HttpTenantProvider : ITenantProvider
{
    private readonly IHttpContextAccessor _http;

    public HttpTenantProvider(IHttpContextAccessor http) => _http = http;

    public Guid? CurrentTenantId
    {
        get
        {
            var header = _http.HttpContext?.Request.Headers["X-Tenant"].ToString();
            return Guid.TryParse(header, out var id) ? id : null;
        }
    }
}
