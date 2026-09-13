using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Model.Projects.Queries.GetUserProjects
{
    public class GetUserProjectsQueryValidator : AbstractValidator<GetUserProjectsQuery>
    {
        public GetUserProjectsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least 1.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 50).WithMessage("PageSize must be between 1 and 50.");
        }
    }
}
