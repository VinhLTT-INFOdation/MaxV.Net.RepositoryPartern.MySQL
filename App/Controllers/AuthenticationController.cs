
using App.Repositories.Interface;
using App.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AutoMapper;
using App.Controllers.Base;
using App.Services.Interface;
using MaxV.Base.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace App.Controllers
{
    public class AuthenticationController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authentication)
        {
            _authenticationService = authentication;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterDTO request)
        {
            try
            {
                var result = await _authenticationService.Register(request);
                if (result)
                    return Ok();
                return BadRequest();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromForm] LoginDTO request)
        {
            var result = await _authenticationService.Login(request);
            if (string.IsNullOrEmpty(result))
                return BadRequest();
            return Ok(new { token = result });
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout(string request)
        {
            await _authenticationService.Logout(request);
            return Ok();
        }

        [HttpGet("validate-token")]
        [Authorize]
        public async Task<IActionResult> ValidateToken([FromHeader] string authorization)
        {
            //var token = Request.Headers[HeaderNames.Authorization];
            var result = await _authenticationService.CheckToken(authorization);
            if (string.IsNullOrEmpty(result))
                return Unauthorized();
            return Ok();
        }
    }
}
