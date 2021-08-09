using ExpBag.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpBag.Application.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(AppUser user);
    }
}
