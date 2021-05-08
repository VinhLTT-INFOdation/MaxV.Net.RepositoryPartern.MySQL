using App.Data.Entities;
using App.DTO;
using App.Services.Interface;
using MaxV.Base.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace App.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IDistributedCache _cache;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IOptions<JwtOptions> _jwtOptions;
        public AuthenticationService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager, IConfiguration configuration, IDistributedCache cache, IOptions<JwtOptions> jwtOptions)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _cache = cache;
            _jwtOptions = jwtOptions;
        }
        public async Task<string> Login(LoginDTO request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
                return null;

            if (!await _userManager.CheckPasswordAsync(user, request.Password))
                return null;

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,
                          user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            userRoles.ToList().ForEach(e =>
            {
                authClaims.Add(new Claim(ClaimTypes.Role, e));
            });

            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: authClaims,
                expires: DateTime.Now.AddHours(5),
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task Logout(string request)
        {
            await _cache.SetStringAsync($"tokens:{request}:deactivated",
               " ", new DistributedCacheEntryOptions
               {
                   AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
               });
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> Register(RegisterDTO request)
        {
            if (request.Password != request.ConfirmPassword)
                    return false;

            using (var transaction = new CommittableTransaction(new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    var userExist = await _userManager.FindByNameAsync(request.UserName);
                    if (userExist != null)
                        return false;
                    var user = new User()
                    {
                        UserName = request.UserName,
                        Email = request.Email,
                        SecurityStamp = Guid.NewGuid().ToString(),
                    };

                    var result = await _userManager.CreateAsync(user, request.Password);

                    if (!await _roleManager.RoleExistsAsync(UserRole.Admin))
                        await _roleManager.CreateAsync(new IdentityRole(UserRole.Admin));
                    if (!await _roleManager.RoleExistsAsync(UserRole.Employee))
                        await _roleManager.CreateAsync(new IdentityRole(UserRole.Employee));

                    await _userManager.AddToRolesAsync(user, request.Roles);

                    transaction.Commit();

                    if (!result.Succeeded)
                        return false;

                    return true;
                }
                catch (Exception ex)
                {
                    //transaction.Rollback();
                    return false;
                }
            }
        }
        public async Task<bool> CheckToken(string token)
        {
            return await _cache.GetStringAsync($"tokens:{token}:deactivated") == null;
        }
    }
}