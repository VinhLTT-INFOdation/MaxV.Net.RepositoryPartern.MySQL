using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DTO
{
    public static class AuthorizeAttributeRole
    {
        public const string Admin = "Admin";
        public const string Employee = "Admin, Employee";
    }
}
