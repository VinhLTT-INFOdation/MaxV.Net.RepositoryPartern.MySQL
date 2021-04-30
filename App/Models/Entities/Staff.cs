using App.Data.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Data.Entities
{
    public class Staff : BaseEntity
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public DateTime? Dob { get; set; }
        public User User { get; set; }
        public virtual Department Department { get; set; }

    }
}
