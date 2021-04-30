using App.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data.Entities;
using App.Data;
using App.DTO;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Helper;
using App.Infrastructures.Dbcontexts;

namespace App.Repositories.Interface
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<IEnumerable<Department>> GetDepartmentsPagingAsync(string filter, int pageIndex, int pageSize)
        {
            var result = await GetNoTrackingEntities().Where(e => e.Name.Contains(filter) || string.IsNullOrEmpty(filter))
                                          .Skip((pageIndex - 1) * pageSize)
                                          .Take(pageSize)
                                          .OrderBy(e => e.Name)
                                          .ToListAsync();
            return result;
        }

        public override async Task<IEnumerable<Department>> GetAllAsync()
        {
            var result = await GetNoTrackingEntities().Include(e => e.Staffs).ToListAsync();
            return result;
        }

        public async Task<Department> PostAsync(DepartmentRequest request)
        {
            if (request == null)
                return null;
            Department obj = new Department()
            {
                Name = request.Name
            };
            var result = await CreateAsync(obj);
            return result;
        }

        public async Task<int> PutAsync(Guid uuid, DepartmentNonRequest request)
        {
            var obj = await Entities.SingleOrDefaultAsync(e => e.Uuid == uuid);
            if (obj == null)
                return 0;
            obj.Name = request.Name;
            obj.UpdateAt = DateTime.Now;
            
            var result = await UpdateAsync(obj);
            return result;
        }

        public async Task<IEnumerable<Department>> GetAllAsync(string filter)
        {
            var entities = await GetNoTrackingEntities().Where(e => e.Name.Contains(filter)).ToListAsync();
            return entities;
        }
    }
}
