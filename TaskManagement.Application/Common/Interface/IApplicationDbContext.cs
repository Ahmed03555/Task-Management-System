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
        DbSet<Comment> comments { get;  }
        DbSet<User> users { get; }
        DbSet<TaskItem> tasks { get; }

        DbSet<Project> projects { get;  }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    } 
    #endregion
}
