using App.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data.Entities;
using App.DTO;

namespace App.Repositories.Interface
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        /// <summary>
        /// filter with Name. if not, pass parameter filter = null
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<IEnumerable<Department>> GetDepartmentsPagingAsync(string filter, int pageIndex, int pageSize);
        /// <summary>
        /// filter with Name. if not, pass parameter filter = null
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Task<IEnumerable<Department>> GetAllAsync(string filter);
        public Task<int> PutAsync(Guid uuid, DepartmentNonRequest request);
        public Task<Department> PostAsync(DepartmentRequest request);
    }
}
