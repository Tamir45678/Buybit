using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Users.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }    

        public string Token { get; set; }
    }
}