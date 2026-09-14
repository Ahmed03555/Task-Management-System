using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Model.Tasks.Commands.UpdateTaskStatus
{
    public class UpdateTaskStatusCommandValidator : AbstractValidator<UpdateTaskStatusCommand>
    {
        public UpdateTaskStatusCommandValidator()
        {
            RuleFor(t => t.Id)
                .NotEmpty().WithMessage("Task Id is required.");

            RuleFor(t => t.TaskStatus)
                .IsInEnum().WithMessage("Invalid task status.");
        }
    }
}
