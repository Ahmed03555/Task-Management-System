using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Common.Interface
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid UserId , string Email);
    }
}
