using App.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserNonRequest>> GetAllAsync(string filter);
        Task<UserNonRequest> GetUserById(string id);
    }
}
