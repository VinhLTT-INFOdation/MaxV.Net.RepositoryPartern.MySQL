using App.Data;
using App.Data.Entities;
using App.DTO;
using App.Infrastructures.Dbcontexts;
using App.Repositories.BaseRepository;
using App.Repositories.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Repositories.Interface
{
    public class StaffRepository : BaseRepository<Staff>, IStaffRepository
    {
        private readonly UserManager<User> _userManager;
        public StaffRepository(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager) : base(context, mapper)
        {
            _userManager = userManager;
        }
        public async Task<IEnumerable<Staff>> GetStaffsPagingAsync(string filter, int pageIndex, int pageSize)
        {
            
            var result = await GetNoTrackingEntities().Where(e => e.FullName.Contains(filter) || e.FirstName.Contains(filter) || e.LastName.Contains(filter) || string.IsNullOrEmpty(filter))
                                          .Skip((pageIndex - 1) * pageSize)
                                          .Take(pageSize)
                                          .OrderBy(e => e.FirstName)
                                          .ThenBy(e => e.LastName)
                                          .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Staff>> GetAllAsync(string filter)
        {
            
            var result = await GetNoTrackingEntities().Where(e => e.FullName.Contains(filter) || e.FirstName.Contains(filter) || e.LastName.Contains(filter) || string.IsNullOrEmpty(filter))
                                          .OrderBy(e => e.FirstName)
                                          .ThenBy(e => e.LastName)
                                          .ToListAsync();
            return result;
        }

        public async Task<Staff> PostAsync(Staff request)
        {            
            return await CreateAsync(request);
        }

        public async Task<int> PutAsync(Guid uuid, Staff request)
        {
            if (uuid != request.Uuid)
                return 0;
            var dateTimeNow = DateTime.Now;
            var obj = await GetByUuidTrackingAsync(uuid);
            obj.LastName = request.LastName;
            obj.FirstName = request.FirstName;
            obj.FullName = request.FullName;
            obj.User = request.User;
            obj.UpdateAt = DateTime.Now;

            return await UpdateAsync(obj);
        }
    }
}
