using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Common.Interface
{
    #region Dependency Inversion Principle
    public interface IApplicationDbContext
    {
        DbSet<Comment> comments { get; set; }
        DbSet<User> users { get; set; }
        DbSet<TaskItem> tasks { get; set; }

        DbSet<Project> projects { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    } 
    #endregion
}
