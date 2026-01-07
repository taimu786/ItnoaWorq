using ItnoaWorq.Domain.Entities;
using ItnoaWorq.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Domain.Entities.Identity;

namespace ItnoaWorq.Infrastructure.Persistence;

public class HrmsDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    private readonly ITenantProvider _tenant;

    public HrmsDbContext(DbContextOptions<HrmsDbContext> options, ITenantProvider tenant) : base(options)
        => _tenant = tenant;

    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<Plan> Plans => Set<Plan>();
    public DbSet<Job> Jobs => Set<Job>();
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<AttendanceLog> AttendanceLogs => Set<AttendanceLog>();
    public DbSet<Request> Requests => Set<Request>();
    public DbSet<Payroll> Payrolls => Set<Payroll>();
    public DbSet<PayrollDetail> PayrollDetails => Set<PayrollDetail>();
    public DbSet<PerformanceReview> PerformanceReviews => Set<PerformanceReview>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<Connection> Connections => Set<Connection>();
    public DbSet<PostReaction> PostReactions => Set<PostReaction>();
    public DbSet<PublicProfile> PublicProfiles => Set<PublicProfile>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<ProfileSkill> ProfileSkills => Set<ProfileSkill>();
    public DbSet<Education> Educations => Set<Education>();
    public DbSet<Experience> Experiences => Set<Experience>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<PostComment> PostComments => Set<PostComment>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        // Indexes/uniques
        b.Entity<Tenant>().HasIndex(x => x.Slug).IsUnique();
        b.Entity<Employee>().HasIndex(x => new { x.TenantId, x.UserId }).IsUnique();
        b.Entity<Employee>().HasIndex(x => new { x.TenantId, x.EmployeeNo }).IsUnique();
        b.Entity<Department>().HasIndex(x => new { x.TenantId, x.Name }).IsUnique();
        b.Entity<Job>().HasIndex(x => new { x.TenantId, x.IsActive, x.PostedAt });
        b.Entity<JobApplication>().HasIndex(x => new { x.TenantId, x.JobId, x.Status });
        b.Entity<AttendanceLog>().HasIndex(x => new { x.TenantId, x.EmployeeId, x.CheckIn });
        b.Entity<Request>().HasIndex(x => new { x.TenantId, x.EmployeeId, x.Status, x.RequestedAt });
        b.Entity<Payroll>().HasIndex(x => new { x.TenantId, x.EmployeeId, x.Year, x.Month }).IsUnique();
        b.Entity<PerformanceReview>().HasIndex(x => new { x.TenantId, x.EmployeeId, x.ReviewPeriod });

        // Global tenant filter
        foreach (var et in b.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(et.ClrType))
            {
                var m = typeof(HrmsDbContext)
                    .GetMethod(nameof(ApplyTenantFilter), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)!
                    .MakeGenericMethod(et.ClrType);
                m.Invoke(null, new object[] { b, this });
            }
        }
    }

    private static void ApplyTenantFilter<TEntity>(ModelBuilder builder, HrmsDbContext ctx) where TEntity : BaseEntity
        => builder.Entity<TEntity>().HasQueryFilter(e => e.TenantId == ctx._tenant.CurrentTenantId || e.TenantId == null);
}
