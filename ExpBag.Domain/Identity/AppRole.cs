using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpBag.Domain
{
    public class AppRole : IdentityRole<long>
    {
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}
