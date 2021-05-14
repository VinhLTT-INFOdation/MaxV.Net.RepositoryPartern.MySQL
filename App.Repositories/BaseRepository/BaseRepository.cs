using App.Data;
using AutoMapper;
using MaxV.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.Repositories.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        #region props

        private readonly DbContext _context;
        private bool _disposed = false;
        private DbSet<T> _entitiesDbSet { get; set; }
        private IDbContextTransaction _tx { get; set; }
        public readonly IMapper _mapper;

        #endregion props

        #region ctor

        public BaseRepository(DbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        ~BaseRepository()
        {
            Dispose(false);
        }

        #endregion ctor

        #region public

        public IQueryable<T> GetQueryableTable()
        {
            return GetNoTrackingEntities();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await GetNoTrackingEntities().ToListAsync();
            return entities;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var entity = await GetNoTrackingEntities().FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public virtual async Task<T> GetByUuidNoTrackingAsync(Guid uuid)
        {
            var entity = await GetNoTrackingEntities().SingleOrDefaultAsync(x => x.Uuid == uuid);
            return entity;
        }

        public virtual async Task<T> GetByUuidTrackingAsync(Guid uuid)
        {
            var entity = await Entities.SingleOrDefaultAsync(x => x.Uuid == uuid);
            return entity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            ValidateAndThrow(entity);
            AddDefaultValue(ref entity);
            Entities.Add(entity);

            var effectedCount = await _context.SaveChangesAsync();
            if (effectedCount == 0)
            {
                return null;
            }
            return entity;
        }

        public async Task<IEnumerable<T>> CreateAsync(List<T> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                ValidateAndThrow(entity);
                AddDefaultValue(ref entity);
            }

            Entities.AddRange(entities);
            var effectedCount = await _context.SaveChangesAsync();
            if (effectedCount == 0)
            {
                return null;
            }
            return entities;
        }

        public virtual async Task<int> UpdateAsync(T entity)
        {
            ValidateAndThrow(entity);
            var entry = _context.Entry(entity);
            if (entry.State < EntityState.Added)
            {
                entry.State = EntityState.Modified;
            }

            entity.UpdateAt = DateTime.UtcNow;
            var effectedCount = await _context.SaveChangesAsync();
            return effectedCount;
        }

        public virtual async Task<int> DeleteHardAsync(Guid uuid)
        {
            var entity = await _context.Set<T>().SingleOrDefaultAsync(e => e.Uuid == uuid);
            ValidateAndThrow(entity);
            Entities.Remove(entity);
            var effectedCount = await _context.SaveChangesAsync();
            return effectedCount;
        }
        public virtual async Task<int> DeleteSoftAsync(Guid uuid)
        {
            var entity = await _context.Set<T>().SingleOrDefaultAsync(e => e.Uuid == uuid);
            ValidateAndThrow(entity);
            entity.Deleted = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var effectedCount = await UpdateAsync(entity);
            return effectedCount;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion public

        #region private

        private void ValidateAndThrow(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }

        private void AddDefaultValue(ref T entity)
        {
            entity.Uuid = Guid.NewGuid();
            entity.CreateAt = DateTime.UtcNow;
            entity.UpdateAt = entity.CreateAt;
        }

        protected DbSet<T> Entities
        {
            get
            {
                if (_entitiesDbSet == null)
                    _entitiesDbSet = _context.Set<T>();
                return _entitiesDbSet;
            }
        }

        protected IQueryable<T> GetNoTrackingEntities()
        {
            var table = Entities.AsNoTracking();
            return table;
        }

        protected async Task BeginTransactionAsync()
        {
            _tx = await _context.Database.BeginTransactionAsync();
        }

        protected async Task CommitTransactionAsync()
        {
            await _tx.CommitAsync();
            await ReleaseTransactionAsync();
        }

        protected async Task RollbackTransactionAsync()
        {
            await _tx.RollbackAsync();
            await ReleaseTransactionAsync();
        }

        protected async Task ReleaseTransactionAsync()
        {
            await _tx.DisposeAsync();
            _tx = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _tx?.Dispose();
                _tx = null;
            }

            _disposed = true;
        }

        #endregion private

    }
}
