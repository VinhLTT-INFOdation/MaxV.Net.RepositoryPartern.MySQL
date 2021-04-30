using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DTO
{
    public class RegisterDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
