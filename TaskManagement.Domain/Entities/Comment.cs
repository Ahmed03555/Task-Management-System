namespace TaskManagement.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; } = default!;
        public Guid? TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; } = default!;

        public Guid? AuthorId { get; set; }
        public  User Author { get; set; } = default!;

    }
}