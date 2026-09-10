using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application
{
    #region ResultEx
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T? Value { get; set; }
        public string? Error { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public Result()
        {

        }

        private Result(bool isSuccess, T? value, string? error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
            
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, value,null);
        }

        public static Result<T> Failure(string error)
        {
            return new Result<T>(false, default, error);
        }

        public static Result<T> Failure(List<string> errors)
        {
            var result = new Result<T>(false, default, errors?.FirstOrDefault());
            if (errors != null && errors.Any())
            {
                result.Errors.AddRange(errors);
            }

            return result;
        }

    } 
    #endregion
}
