using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProAgil.Domain.Identity
{
    public class UserRoles : IdentityUserRole<int>
    {
        public User User { get; set; }
        public Roles Roles { get; set; }
    }
}
