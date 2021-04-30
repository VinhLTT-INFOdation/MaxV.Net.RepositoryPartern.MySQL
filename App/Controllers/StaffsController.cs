using App.Controllers.Base;
using App.DTO;
using App.Repositories.Interface;
using App.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppS4.Controllers
{
    public class StaffsController : ApiController
    {
        public readonly IStaffService _staffService;
        public StaffsController(IStaffService staffService)
        {
            _staffService = staffService;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _staffService.GetAllDTOAsync();
            return Ok(result);
        }
        [HttpGet("{uuid}")]
        public async Task<ActionResult> GetById(Guid uuid)
        {
            var result = await _staffService.GetByUuidDTOAsync(uuid);
            return Ok(result);
        }
        [HttpGet("filterPaging")]
        public async Task<ActionResult> GetsWithPaging(string filter, int pageIndex, int pageSize)
        {
            var result = await _staffService.GetStaffsPagingAsync(filter, pageIndex, pageSize);
            return Ok(result);
        }
        [HttpGet("filter")]
        public async Task<ActionResult> Gets(string filter)
        {
            var result = await _staffService.GetAllAsync(filter);
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult> Put(Guid uuid, StaffNonRequest request)
        {
            if (uuid != request.Uuid)
                return BadRequest();
            var result = await _staffService.PutAsync(uuid, request);
            if (result > 0)
                return Ok();
            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromForm]StaffRequest request)
        {
            var result = await _staffService.PostAsync(request);

            if (result != null)
                return Ok(result);
            return NotFound();
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(Guid uuid)
        {
            int result = await _staffService.DeleteSoftAsync(uuid);
            if (result > 0)
                return Ok();
            return NotFound();
        }
    }
}
