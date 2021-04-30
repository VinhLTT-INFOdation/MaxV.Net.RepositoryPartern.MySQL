using App.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.Interface
{
    public interface IAuthenticationService
    {
        Task<bool> Register(RegisterDTO request);
        Task<string> Login(LoginDTO request);
        Task Logout(string request);
        Task<bool> CheckToken(string token);
    }
}
