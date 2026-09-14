using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Task_Management_System.Controllers;
using Task_Management_System.Middlewares;
using TaskManagement.Application.Common.Behaviors;
using TaskManagement.Application.Model;
using TaskManagement.Infrastructure;
using TaskManagement.Infrastructure.Persistence;
using TaskManagement.Application.Common.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region DependencyInjection
builder.Services.AddApplicationDependencyInjection();
builder.Services.AddInfrastructureDependencyInjection(builder.Configuration); 
#endregion
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Input of Authorization
builder.Services.AddSwaggerGen(options =>
{
options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
Name = "Authorization",
Type = SecuritySchemeType.ApiKey,
Scheme = "Bearer",
BearerFormat = "JWT",
In = ParameterLocation.Header,
Description = "Enter: Bearer {your JWT token}"
});

options.AddSecurityRequirement(new OpenApiSecurityRequirement
{
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
});
});

#endregion
var app = builder.Build();

#region MyRegion
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
    var adminSettings = scope.ServiceProvider.GetRequiredService<IOptions<AdminSeedSettings>>().Value;

    await dbContext.Database.MigrateAsync();
    await DbSeeder.SeedAdminUserAsync(dbContext, passwordHasher, adminSettings,CancellationToken.None);
}
#endregion
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
