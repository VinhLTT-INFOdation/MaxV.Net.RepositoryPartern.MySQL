using App.Controllers.Base;
using App.DTO;
using App.Repositories.Interface;
using App.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppS4.Controllers
{
    public class DepartmentsController : ApiController
    {
        public readonly IDepartmentService _departmentService;
        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet("")]
        public async Task<ActionResult> GetAll()
        {
            var result = await _departmentService.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{uuid}")]
        public async Task<ActionResult> GetById(Guid uuid)
        {
            var result = await _departmentService.GetByUuidDTOAsync(uuid);
            return Ok(result);
        }
        [HttpGet("filterPaging")]
        public async Task<ActionResult> GetsWithPaging(string filter, int pageIndex, int pageSize)
        {
            var result = await _departmentService.GetDepartmentsPagingAsync(filter, pageIndex, pageSize);
            return Ok(result);
        }
        [HttpGet("filter")]
        public async Task<ActionResult> Gets(string filter)
        {
            var result = await _departmentService.GetAllAsync(filter);
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult> Put(Guid uuid, DepartmentNonRequest request)
        {
            if (uuid != request.Uuid)
                return BadRequest();
            var result = await _departmentService.PutAsync(uuid, request);
            if(result > 0)
                return Ok();
            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromForm]DepartmentRequest request)
        {
            var result = await _departmentService.PostAsync(request);

            if (result != null)
                return Ok(result);
            return NotFound();
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(Guid uuid)
        {
            var result = await _departmentService.DeleteSoftAsync(uuid);
            if (result == 1)
                return Ok();
            return BadRequest();
        }
    }
}
