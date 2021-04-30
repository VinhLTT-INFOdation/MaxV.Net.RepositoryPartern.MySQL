using App.Data.Entities;
using App.DTO;
using App.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Repositories.Interface
{
    public interface IStaffRepository : IBaseRepository<Staff>
    {
        /// <summary>
        /// filter with FullName,FirstName,LastName. if not, pass parameter filter = null
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<IEnumerable<Staff>> GetStaffsPagingAsync(string filter, int pageIndex, int pageSize);
        public Task<Staff> PostAsync(Staff request);
        /// <summary>
        /// filter with FullName,FirstName,LastName. if not, pass parameter filter = null
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Task<IEnumerable<Staff>> GetAllAsync(string filter);
        public Task<int> PutAsync(Guid uuid, Staff request);
    }
}
