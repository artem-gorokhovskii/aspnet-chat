using chat.Entities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat.Policy
{
    public class RolePolicy : IAuthorizationRequirement
    {
        public string role;
        public RolePolicy(string role)
        {
            this.role = role;
        }
    }
}
