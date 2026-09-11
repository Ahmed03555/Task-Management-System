using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Common.Exceptions
{
    #region ValidationException
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(IEnumerable<ValidationFailure> failures) : base("One or more validation failures occurred.")
        {
            Errors = failures
                .GroupBy(x => x.PropertyName)
                .ToDictionary(x => x.Key, x => x.Select(f => f.ErrorMessage).ToArray());
        }
    } 
    #endregion
}
