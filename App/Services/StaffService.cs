using App.Data.Entities;
using App.DTO;
using App.Repositories.Interface;
using App.Services.Base;
using App.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services
{
    public class StaffService : BaseService<Staff, StaffRequest, StaffNonRequest>, IStaffService
    {
        public readonly IStaffRepository _staffRepository;
        public readonly IDepartmentRepository _departmentRepository;
        public StaffService(IStaffRepository staffRepository, IMapper mapper, IDepartmentRepository departmentRepository) : base(staffRepository, mapper)
        {
            _staffRepository = staffRepository;
            _departmentRepository = departmentRepository;
        }
        public async Task<int> PutAsync(Guid uuid, StaffNonRequest request)
        {
            if (uuid != request.Uuid)
                return 0;

            var entity = await _repository.GetByUuidTrackingAsync(request.Uuid);
            if (entity == null)
                return 0;
            var dateTimeNow = DateTime.UtcNow;

            entity.Department = await _departmentRepository.GetByUuidTrackingAsync(request.DepartmentUuid);
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.FullName = request.FirstName + " " + request.LastName;
            entity.Dob = request.Dob;
            entity.UpdateAt = dateTimeNow;

            var result = await _repository.UpdateAsync(entity);
            return result;
        }

        public async Task<StaffNonRequest> PostAsync(StaffRequest request)
        {
            var entity = new Staff()
            {
                Uuid = Guid.NewGuid(),
                Department = await _departmentRepository.GetByUuidTrackingAsync(request.DepartmentUuid),
                FirstName = request.FirstName,
                LastName = request.LastName,
                FullName = request.FirstName + " " + request.LastName,
                Dob = request.Dob,
            };

            var response = await _repository.CreateAsync(entity);

            var result = _mapper.Map<StaffNonRequest>(response);
            return result;
        }

        public async Task<IEnumerable<StaffNonRequest>> GetStaffsPagingAsync(string filter, int pageIndex, int pageSize)
        {
            var entities = await _staffRepository.GetStaffsPagingAsync(filter, pageIndex, pageSize);
            var result = _mapper.Map<IEnumerable<StaffNonRequest>>(entities);
            return result;
        }

        public async Task<IEnumerable<StaffNonRequest>> GetAllAsync(string filter = "")
        {
            var entities = await _staffRepository.GetAllAsync(filter);
            var result = _mapper.Map<IEnumerable<StaffNonRequest>>(entities);
            return result;
        }
    }
}
