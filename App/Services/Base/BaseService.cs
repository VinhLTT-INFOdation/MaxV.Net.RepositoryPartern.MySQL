using App.Data.Base;
using App.DTO.Base;
using App.Repositories.BaseRepository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.Base
{
    public class BaseService<T, TRequest, TNonRequest> : IBaseService<T, TRequest, TNonRequest> where T : BaseEntity
    {
        public IBaseRepository<T> _repository;
        public readonly IMapper _mapper;
        public BaseService(IBaseRepository<T> baseRepository, IMapper mapper)
        {
            _repository = baseRepository;
            _mapper = mapper;
        }

        public async Task<int> DeleteHardAsync(Guid uuid)
        {
            return await _repository.DeleteHardAsync(uuid);
        }

        public async Task<int> DeleteSoftAsync(Guid uuid)
        {
            return await _repository.DeleteSoftAsync(uuid);
        }

        public async Task<IEnumerable<TNonRequest>> GetAllDTOAsync()
        {
            var entities = await _repository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<TNonRequest>>(entities);
            return result;
        }

        public async Task<TNonRequest> GetByUuidDTOAsync(Guid uuid)
        {
            var entity = await _repository.GetByUuidNoTrackingAsync(uuid);
            var result = _mapper.Map<TNonRequest>(entity);
            return result;
        }
    }
}
