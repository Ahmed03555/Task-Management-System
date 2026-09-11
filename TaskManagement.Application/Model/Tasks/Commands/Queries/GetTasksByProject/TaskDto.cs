using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Model.Tasks.Commands.Queries.GetTasksByProject
{
    #region TaskDto
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public string Status { get; set; } = default!;
        public string Priority { get; set; } = default!;
        public DateTime? DueDate { get; set; }
    } 
    #endregion
}
