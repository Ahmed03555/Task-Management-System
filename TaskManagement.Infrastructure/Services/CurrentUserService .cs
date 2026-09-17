using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common.Interface;

namespace TaskManagement.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor=httpContextAccessor;
        }

        public Guid? UserId
        {
            get{
                var userIdClaim = _httpContextAccessor.HttpContext?.User?
    .FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
               return Guid.TryParse(userIdClaim, out var userId) ? userId : null;
            }
        }

        public bool IsAdmin => _httpContextAccessor.HttpContext?.User.IsInRole("Admin") ?? false;
    }
}
