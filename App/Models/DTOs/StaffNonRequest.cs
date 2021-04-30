using App.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DTO
{
    public class StaffNonRequest : BaseDTO
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public DateTime? Dob { get; set; }
        public string UserId { get; set; }
        public string UserUserName { get; set; }
        public Guid DepartmentUuid { get; set; }
        public string DepartmentName { get; set; }
    }
}
