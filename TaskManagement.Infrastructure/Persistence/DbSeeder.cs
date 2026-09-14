using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Common.Interface;
using TaskManagement.Application.Model;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Infrastructure.Persistence
{
    public static class DbSeeder
    {
        public static async Task SeedAdminUserAsync(
            IApplicationDbContext applicationDbContext,
            IPasswordHasher passwordHasher,
            AdminSeedSettings adminSeedSettings,
            CancellationToken cancellationToken)
        {
            var adminExists = await applicationDbContext.users
                .AnyAsync(u => u.Role == UserRole.Admin, cancellationToken);

            if (adminExists)
                return;

            var admin = new User
            {
                FullName = adminSeedSettings.FullName,
                Email = adminSeedSettings.Email,
                PasswordHash = passwordHasher.Hash(adminSeedSettings.Password),
                Role = UserRole.Admin
            };

            applicationDbContext.users.Add(admin);
            await applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}