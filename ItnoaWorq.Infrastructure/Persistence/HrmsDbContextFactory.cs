using ItnoaWorq.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ItnoaWorq.Infrastructure.Persistence
{
    public class HrmsDbContextFactory : IDesignTimeDbContextFactory<HrmsDbContext>
    {
        public HrmsDbContext CreateDbContext(string[] args)
        {
            // Load appsettings from API project (assuming standard layout)
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ItnoaWorq.Api"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<HrmsDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));

            // Pass null for ITenantProvider (only needed at runtime)
            return new HrmsDbContext(optionsBuilder.Options, new DesignTimeTenantProvider());
        }

        // simple stub tenant provider for migrations
        private class DesignTimeTenantProvider : ItnoaWorq.Application.Abstraction.Interfaces.ITenantProvider
        {
            public Guid? CurrentTenantId => null;
        }
    }
}
