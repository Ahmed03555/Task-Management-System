using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Model.Users.Commands.Queries.GetAllUsers
{
    public class GetAllUsersQueryValidator : AbstractValidator<GetAllUsersQuery>
    {
        public GetAllUsersQueryValidator()
        {
            RuleFor(u => u.PageNumber)
                 .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least 1.");

            RuleFor(u => u.PageSize)
                 .InclusiveBetween(1, 50).WithMessage("PageSize must be between 1 and 50.");
        }
    }
}
