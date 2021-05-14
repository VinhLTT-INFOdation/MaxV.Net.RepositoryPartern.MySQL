using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DTO
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public string ValidAudience { get; set; }
        public int ExpiryMinutes { get; set; }
        public string ValidIssuer { get; set; }
    }
}
