using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.Repositories.BaseRepository
{
    public interface IBaseRepository<T> : IDisposable
    {
        public IQueryable<T> GetQueryableTable();
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task<T> GetByUuidTrackingAsync(Guid uuid);
        public Task<T> GetByUuidNoTrackingAsync(Guid uuid);
        public Task<T> CreateAsync(T entity);
        public Task<IEnumerable<T>> CreateAsync(List<T> entities);
        public Task<int> UpdateAsync(T entity);
        public Task<int> DeleteHardAsync(Guid uuid);
        public Task<int> DeleteSoftAsync(Guid uuid);
    }
}
