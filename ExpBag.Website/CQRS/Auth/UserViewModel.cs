using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpBag.Website.CQRS.Auth
{
    public class UserViewModel
    {
        public string DisplayName { get; set; }

        public string Token { get; set; }

        public string UserName { get; set; }

        public string Image { get; set; }
    }
}
