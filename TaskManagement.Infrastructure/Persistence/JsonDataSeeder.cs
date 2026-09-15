// Infrastructure/Persistence/JsonDataSeeder.cs
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TaskManagement.Application.Common.Interface;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Infrastructure.Persistence
{
    public static class JsonDataSeeder
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public static async Task SeedFromJsonAsync(
            IApplicationDbContext dbContext,
            CancellationToken cancellationToken)
        {
            if (await dbContext.users.CountAsync(cancellationToken) > 1)
                return;

            var basePath = Path.Combine(AppContext.BaseDirectory, "Persistence", "SeedData");

            var usersJson = await File.ReadAllTextAsync(Path.Combine(basePath, "users.json"), cancellationToken);
            var userDtos = JsonSerializer.Deserialize<List<UserSeedDto>>(usersJson, Options) ?? new();

            var users = userDtos.Select(u => new User
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("User@123456"),
                Role = Enum.Parse<UserRole>(u.Role),
                CreatedAt = u.CreatedAt
            }).ToList();

            dbContext.users.AddRange(users);
            await dbContext.SaveChangesAsync(cancellationToken);

        
            var projectsJson = await File.ReadAllTextAsync(Path.Combine(basePath, "projects.json"), cancellationToken);
            var projectDtos = JsonSerializer.Deserialize<List<ProjectSeedDto>>(projectsJson, Options) ?? new();

            var projects = projectDtos.Select(p => new Project
            {
                Id = p.Id,
                ProjectName = p.ProjectName,
                Description = p.Description,
                OwnerId = p.OwnerId,
                CreatedAt = p.CreatedAt
            }).ToList();

            dbContext.projects.AddRange(projects);
            await dbContext.SaveChangesAsync(cancellationToken);

         
            var tasksJson = await File.ReadAllTextAsync(Path.Combine(basePath, "tasks.json"), cancellationToken);
            var taskDtos = JsonSerializer.Deserialize<List<TaskSeedDto>>(tasksJson, Options) ?? new();

            var tasks = taskDtos.Select(t => new TaskItem
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Satus = Enum.Parse<TaskeStatus>(t.Status),
                Priority = Enum.Parse<TaskPriority>(t.Priority),
                DueDate = t.DueDate,
                ProjectId = t.ProjectId,
                CreatedAt = t.CreatedAt
            }).ToList();

            dbContext.tasks.AddRange(tasks);
            await dbContext.SaveChangesAsync(cancellationToken);

           
            var commentsJson = await File.ReadAllTextAsync(Path.Combine(basePath, "comments.json"), cancellationToken);
            var commentDtos = JsonSerializer.Deserialize<List<CommentSeedDto>>(commentsJson, Options) ?? new();

            var comments = commentDtos.Select(c => new Comment
            {
                Id = c.Id,
                Content = c.Content,
                TaskItemId = c.TaskItemId,
                AuthorId = c.AuthorId,
                CreatedAt = c.CreatedAt
            }).ToList();

            dbContext.comments.AddRange(comments);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

  
    internal class UserSeedDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    internal class ProjectSeedDto
    {
        public Guid Id { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    internal class TaskSeedDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    internal class CommentSeedDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public Guid TaskItemId { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}