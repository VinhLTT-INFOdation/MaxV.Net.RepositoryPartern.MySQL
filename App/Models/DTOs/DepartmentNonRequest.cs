using App.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DTO
{
    public class DepartmentNonRequest : BaseDTO
    {
        public string Name { get; set; }
        public ICollection<StaffNonRequest> Staffs { get; set; }
    }
}
