using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Model.Projects.Common
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public string OwnerName { get; set; } = string.Empty;
    }
}
