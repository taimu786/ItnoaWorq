using ItnoaWorq.Application;
using ItnoaWorq.Infrastructure;
using ItnoaWorq.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
    options.AddPolicy("AllowLocal", p => p
      .WithOrigins("http://localhost:3000")
      .AllowCredentials()
      .AllowAnyHeader()
      .AllowAnyMethod());
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowLocal");

// Seeding
await IdentitySeed.SeedRolesAndSuperAdminAsync(app.Services);
await PlanSeed.SeedPlansAndDemoTenantAsync(app.Services);

app.Run();
