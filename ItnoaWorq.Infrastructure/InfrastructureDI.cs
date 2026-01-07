using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Domain.Entities;
using ItnoaWorq.Domain.Entities.Identity;
using ItnoaWorq.Infrastructure.Persistence;
using ItnoaWorq.Infrastructure.Repositories;
using ItnoaWorq.Infrastructure.Security;
using ItnoaWorq.Infrastructure.Tenancy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ItnoaWorq.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        // Always first
        services.AddHttpContextAccessor();

        // DbContext first (Identity depends on it)
        services.AddDbContext<HrmsDbContext>(opt =>
            opt.UseSqlServer(config.GetConnectionString("Default")));

        // ✅ Identity next
        services.AddIdentityCore<AppUser>(o => o.User.RequireUniqueEmail = true)
            .AddRoles<AppRole>()
            .AddEntityFrameworkStores<HrmsDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        // ✅ Now you can safely register the repository that depends on UserManager
        services.AddScoped<IUserRepository, UserRepository>();

        // Remaining registrations
        services.Configure<JwtOptions>(config.GetSection(JwtOptions.SectionName));

        var jwt = config.GetSection(JwtOptions.SectionName).Get<JwtOptions>()!;
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))
                };
            });

        services.AddAuthorization();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<ITenantProvider, HttpTenantProvider>();

        return services;
    }

}
