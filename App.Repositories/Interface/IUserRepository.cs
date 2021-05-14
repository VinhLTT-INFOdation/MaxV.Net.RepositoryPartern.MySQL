using App.Data.Entities;
using App.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync(string filter);
        Task<User> GetUserById(string id);
        //Task<int?> PutUser(string id, UserRequest request);
        //Task<int?> PostUser(UserNonRequest request);
    }
}
