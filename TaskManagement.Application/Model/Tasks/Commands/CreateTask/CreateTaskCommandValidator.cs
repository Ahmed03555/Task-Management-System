using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Model.Tasks.Commands.CreateTask
{
    #region CreateTaskCommandValidator
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(t => t.Title).NotEmpty()
                .WithMessage("Title is required")
                .MaximumLength(200);

            RuleFor(t => t.ProjectId).NotEmpty()
                .WithMessage("Project Id is required");
        }
    } 
    #endregion
}
