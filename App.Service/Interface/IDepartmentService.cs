using App.Data.Entities;
using App.DTO;
using App.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.Interface
{
    public interface IDepartmentService : IBaseService<Department, DepartmentRequest,DepartmentNonRequest>
    {
        public Task<int> PutAsync(Guid uuid, DepartmentNonRequest request);
        public Task<DepartmentNonRequest> PostAsync(DepartmentRequest request);
        /// <summary>
        /// filter with Name. if not, pass parameter filter = null
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<IEnumerable<DepartmentNonRequest>> GetDepartmentsPagingAsync(string filter, int pageIndex, int pageSize);
        /// <summary>
        /// filter with Name. if not, pass parameter filter = null
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Task<IEnumerable<DepartmentNonRequest>> GetAllAsync(string filter = "");
    }
}
