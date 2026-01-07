using ItnoaWorq.Domain.Entities;
using ItnoaWorq.Domain.Enums;
using ItnoaWorq.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace ItnoaWorq.Infrastructure.Seeders;

public static class PlanSeed
{
    public static async Task SeedPlansAndDemoTenantAsync(IServiceProvider sp)
    {
        using var scope = sp.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<HrmsDbContext>();

        var plans = new[]
        {
            new Plan { Name = "ItnoaBasic", Category = PlanCategory.Personal, Type = PlanType.Free, IsActive = true },
            new Plan { Name = "ItnoaPro", Category = PlanCategory.Personal, Type = PlanType.Paid, IsActive = true },
            new Plan { Name = "ItnoaBusinessBasic", Category = PlanCategory.Business, Type = PlanType.Free, IsActive = true },
            new Plan { Name = "ItnoaBusinessPro", Category = PlanCategory.Business, Type = PlanType.Paid, IsActive = true }
        };

        foreach (var p in plans)
            if (!db.Plans.Any(x => x.Name == p.Name))
                db.Plans.Add(p);

        await db.SaveChangesAsync();

        var demoTenantSlug = "demo-company";
        if (!db.Tenants.Any(t => t.Slug == demoTenantSlug))
        {
            var plan = db.Plans.First(x => x.Name == "ItnoaBusinessBasic");
            db.Tenants.Add(new Tenant
            {
                Name = "Demo Company",
                Slug = demoTenantSlug,
                CurrentPlanId = plan.Id
            });
            await db.SaveChangesAsync();
        }
    }
}
