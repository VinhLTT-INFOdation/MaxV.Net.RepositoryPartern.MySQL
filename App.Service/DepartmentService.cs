using App.Data.Entities;
using App.DTO;
using App.Repositories.Interface;
using App.Services.Base;
using App.Services.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services
{
    public class DepartmentService : BaseService<Department, DepartmentRequest, DepartmentNonRequest>, IDepartmentService
    {
        public readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository,IMapper mapper) : base(departmentRepository,mapper)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<int> PutAsync(Guid uuid, DepartmentNonRequest request)
        {
            if(uuid != request.Uuid)
                return 0;

            var entity = await _departmentRepository.GetByUuidTrackingAsync(request.Uuid);
            if (entity == null)
                return 0;
            var dateTimeNow = DateTime.UtcNow;

            entity.Name = request.Name;
            entity.UpdateAt = dateTimeNow;

            var result = await _repository.UpdateAsync(entity);
            return result;
        }

        public async Task<DepartmentNonRequest> PostAsync(DepartmentRequest request)
        {
            var entity = new Department()
            {
                Name = request.Name
            };
            var response = await _repository.CreateAsync(entity);

            var result = _mapper.Map<DepartmentNonRequest>(response);
            return result;
        }

        public async Task<IEnumerable<DepartmentNonRequest>> GetDepartmentsPagingAsync(string filter, int pageIndex, int pageSize)
        {
            var entities = await _departmentRepository.GetDepartmentsPagingAsync(filter, pageIndex, pageSize);
            var result = _mapper.Map<IEnumerable<DepartmentNonRequest>>(entities);
            return result;
        }

        public async Task<IEnumerable<DepartmentNonRequest>> GetAllAsync(string filter = "")
        {
            var entities = await _departmentRepository.GetAllAsync(filter);
            var result = _mapper.Map<IEnumerable<DepartmentNonRequest>>(entities);
            return result;
        }
    }
}
