using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Common.Interface;

using TaskManagement.Application.Model.Tasks.Commands.CreateTask;

using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;
using Xunit;

namespace TaskManagement.UnitTests.Tasks
{
    public class CreateTaskCommandHandlerTests
    {
        private static IApplicationDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<FakeDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new FakeDbContext(options);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_ProjectDoesNotExist()
        {
            // Arrange
            var context = CreateInMemoryContext();
            var handler = new CreateTaskCommandHandler(context);

            var command = new CreateTaskCommand(
                Title: "Test Task",
                Description: "Test Description",
                Priority: TaskPriority.Medium,
                ProjectId: Guid.NewGuid());

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Project is not Found");
        }

        [Fact]
        public async Task Handle_Should_CreateTask_When_ProjectExists()
        {
            // Arrange
            var context = CreateInMemoryContext();

            var project = new Project
            {
                ProjectName = "Test Project",
                OwnerId = Guid.NewGuid()
            };
            await context.projects.AddAsync(project);
            await context.SaveChangesAsync(CancellationToken.None);

            var handler = new CreateTaskCommandHandler(context);

            var command = new CreateTaskCommand(
                Title: "Test Task",
                Description: "Test Description",
                Priority: TaskPriority.High,
                ProjectId: project.Id);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBe(Guid.Empty);

            var savedTask = await context.tasks.FirstOrDefaultAsync(t => t.Id == result.Value);
            savedTask.Should().NotBeNull();
            savedTask!.Title.Should().Be("Test Task");
            savedTask.Priority.Should().Be(TaskPriority.High);
        }
    }
}