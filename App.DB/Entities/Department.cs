using MaxV.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Data.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<Staff> Staffs { get; set; }
    }
}
