using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.Base
{
    public interface IBaseService<T, TRequest, TNonRequest>
    {
        public Task<IEnumerable<TNonRequest>> GetAllDTOAsync();
        //public Task<TNonRequest> GetByIdAsync(int id);
        public Task<TNonRequest> GetByUuidDTOAsync(Guid uuid);
        public Task<int> DeleteHardAsync(Guid uuid);
        public Task<int> DeleteSoftAsync(Guid uuid);
    }
}
