using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Common.Interface
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string Key ,CancellationToken cancellationToken);
        Task SetAsync<T>(string key, T Value, TimeSpan expiration, CancellationToken cancellationToken);

        Task RemoveAsync(string key, CancellationToken cancellationToken);
    }
}
