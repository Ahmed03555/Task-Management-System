using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Common.Interface
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
    }
}
