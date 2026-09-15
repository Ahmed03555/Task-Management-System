using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Model.Comments.Queries
{
    public class GetTaskCommentsQueryValidetor : AbstractValidator<GetTaskCommentsQuery>
    {
        public GetTaskCommentsQueryValidetor()
        {
            RuleFor(c=> c.PageNamber)
                   .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least 1.");

            RuleFor(c => c.PageSize)
                 .InclusiveBetween(1, 50).WithMessage("PageSize must be between 1 and 50.");
        }
    }
}
