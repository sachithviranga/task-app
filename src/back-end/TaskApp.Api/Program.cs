using Microsoft.EntityFrameworkCore;
using TaskApp.Application.Interface;
using TaskApp.Application.Services;
using TaskApp.Infrastructure.Data;
using TaskApp.Infrastructure.Repositories;
using TaskApp.Api.Services;
using TaskApp.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS
var origins = builder.Configuration.GetSection("Cors:Origins").Get<string[]>() ?? [];
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins(origins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var cs = builder.Configuration["Database:ConnectionString"];
// Add DbContext
builder.Services.AddDbContext<TaskAppDbContext>(options =>
    options.UseSqlServer(cs));

// Register repositories
builder.Services.AddScoped<ITasksRepository, TasksRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();

// Register services
builder.Services.AddScoped<ITasksService, TasksService>();
builder.Services.AddScoped<IStatusService, StatusService>();

// Register authentication service
builder.Services.AddScoped<BasicAuthService>();

// Register data seeding service
builder.Services.AddScoped<DataSeedingService>();

var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var seedingService = scope.ServiceProvider.GetRequiredService<DataSeedingService>();
    await seedingService.SeedStatusDataAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowAngularApp");

// Add basic authentication middleware
app.UseMiddleware<BasicAuthMiddleware>();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
