using App.DTO;
using MaxV.Base.DTOs;
using System.Threading.Tasks;

namespace App.Services.Interface
{
    public interface IAuthenticationService
    {
        Task<bool> Register(RegisterDTO request);
        Task<string> Login(LoginDTO request);
        Task Logout(string request);
        Task<string> CheckToken(string token);
    }
}
