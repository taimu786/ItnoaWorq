using Microsoft.Extensions.DependencyInjection;
using MediatR;
using FluentValidation;

namespace ItnoaWorq.Application;

public static class ApplicationDI
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(ApplicationDI).Assembly));

        services.AddValidatorsFromAssembly(typeof(ApplicationDI).Assembly);

        return services;
    }
}
