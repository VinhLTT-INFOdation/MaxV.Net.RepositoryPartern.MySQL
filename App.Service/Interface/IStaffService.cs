using App.Data.Entities;
using App.DTO;
using App.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.Interface
{
    public interface IStaffService : IBaseService<Staff, StaffRequest, StaffNonRequest>
    {
        public Task<int> PutAsync(Guid uuid, StaffNonRequest request);
        public Task<StaffNonRequest> PostAsync(StaffRequest request);
        /// <summary>
        /// filter with Name. if not, pass parameter filter = null
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<IEnumerable<StaffNonRequest>> GetStaffsPagingAsync(string filter, int pageIndex, int pageSize);
        /// <summary>
        /// filter with Name. if not, pass parameter filter = null
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Task<IEnumerable<StaffNonRequest>> GetAllAsync(string filter = "");
    }
}
