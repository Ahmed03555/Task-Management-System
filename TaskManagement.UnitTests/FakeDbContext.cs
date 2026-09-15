using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common.Interface;
using TaskManagement.Domain.Entities;

namespace TaskManagement.UnitTests
{
    public class FakeDbContext : DbContext, IApplicationDbContext
    {
        public FakeDbContext(DbContextOptions<FakeDbContext> options) : base(options) { }

        public DbSet<User> users => Set<User>();
        public DbSet<Project> projects => Set<Project>();
        public DbSet<TaskItem> tasks => Set<TaskItem>();
        public DbSet<Comment> comments => Set<Comment>();

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => base.SaveChangesAsync(cancellationToken);
    }
}
