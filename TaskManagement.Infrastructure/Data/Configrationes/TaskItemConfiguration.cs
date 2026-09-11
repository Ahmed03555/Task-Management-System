using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Data.Configrationes
{
    #region TaskItemConfiguration
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Satus).HasConversion<string>().HasMaxLength(50);

            builder.Property(t => t.Priority).HasConversion<string>().HasMaxLength(50);

            builder.HasMany(t => t.Comments)
                .WithOne(c => c.TaskItem)
                .HasForeignKey(c => c.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    } 
    #endregion
}
