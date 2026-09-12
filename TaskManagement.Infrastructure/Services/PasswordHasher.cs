using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common.Interface;

namespace TaskManagement.Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        #region PasswordHasher
        public string Hash(string password)
        {
            // Implementation for hashing password
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string password, string hashedPassword)
        {
            // Implementation for verifying password
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        } 
        #endregion
    }
}
