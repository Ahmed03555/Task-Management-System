using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common.Interface;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Persistence
{
    #region ApplicationDbContext
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Comment> comments => Set<Comment>();
        public DbSet<User> users => Set<User>();
        public DbSet<TaskItem> tasks => Set<TaskItem>();
        public DbSet<Project> projects => Set<Project>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    } 
    #endregion
}
