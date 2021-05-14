using App.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Repositories.Interface
{

    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        public readonly IMapper _mapper;
        public UserRepository(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<User> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            //var result = _mapper.Map<UserNonRequest>(user);
            return user;
        }
        public async Task<IEnumerable<User>> GetAllAsync(string filter)
        {
            var result = await  _userManager.Users
                                            .Where(e =>(e.UserName.Contains(filter) || e.PhoneNumber.Contains(filter)))
                                            .ToListAsync();
            return result;
        }

        //public async Task<int?> PostUser(UserNonRequest request)
        //{

        //}

        //public async Task<int?> PutUser(string id, UserRequest request)
        //{
        //    //if (id != request.Id)
        //    //{
        //    //    return BadRequest();
        //    //}

        //    var user = await _userManager.FindByIdAsync(id);

        //    if (user == null)
        //        return null;

        //    user.PasswordHash = request.Password;
        //    user.Role = await _context.Roles.FindAsync(request.RoleId);
        //    user.Status = request.Status.Value;

        //    _context.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}
    }
}
