using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Model.Projects.Commands.CreateProject
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Project name is required.")
                .MaximumLength(100).WithMessage("Project name must not exceed 100 characters.");
        }
    }
}
