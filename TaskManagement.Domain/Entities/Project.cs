namespace TaskManagement.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string ProjectName { get; set; } = default!;
        public string? Description { get; set; } = default!;

        public Guid OwnerId { get; set; }
        public User Owner { get; set; } = default!;

        public List<TaskItem> Tasks { get; set; } = new();
    }
}