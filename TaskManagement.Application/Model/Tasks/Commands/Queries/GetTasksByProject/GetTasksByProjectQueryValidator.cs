using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Model.Tasks.Commands.Queries.GetTasksByProject
{
    public class GetTasksByProjectQueryValidator : AbstractValidator<GetTasksByProjectQuery>
    {
        public GetTasksByProjectQueryValidator()
        {
            RuleFor(x => x.PageNumber)
               .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least 1.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 50).WithMessage("PageSize must be between 1 and 50.");
        }
    }
}
