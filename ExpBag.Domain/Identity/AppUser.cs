using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpBag.Domain
{
    public class AppUser : IdentityUser<long>
    {
        public string DisplayName { get; set; }
        public string Image { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}
