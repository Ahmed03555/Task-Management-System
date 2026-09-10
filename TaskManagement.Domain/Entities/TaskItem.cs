using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities
{
    public class TaskItem : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public DateTime DueDate { get; set; }

        public TaskeStatus status { get; set; } = TaskeStatus.ToDo;
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
        public Guid? ProjectId { get; set; }
        public Project Project { get; set; } = default!;

        public List<Comment> Comments { get; set; } = new();
    }
}