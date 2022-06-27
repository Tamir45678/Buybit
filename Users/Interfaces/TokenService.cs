using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Entities;

namespace Users.Interfaces
{
    public interface ITokenService
    {
        string CreateToken (AppUser user);
    }
}