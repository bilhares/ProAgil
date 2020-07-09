using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProAgil.Domain.Identity
{
    public class Roles : IdentityRole<int>
    {
        public List<UserRoles> UserRoles { get; set; }
    }
}
