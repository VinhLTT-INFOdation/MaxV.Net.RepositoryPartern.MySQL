using App.Controllers.Base;
using App.DTO;
using App.Repositories.Interface;
using App.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Controllers
{
    public class UsersController : ApiController
    {
        public readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]

        [Authorize(Roles = AuthorizeAttributeRole.Admin)]
        public async Task<IEnumerable<UserNonRequest>> GetUsers()
        {
            var result = await _userService.GetAllAsync("");
            return result;
        }
    }
}
