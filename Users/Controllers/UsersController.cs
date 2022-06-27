using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Data;
using Users.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Users.DTOs;

namespace Users.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UserInfoDto>>> GetUsers(){
            var users = await _context.Users.ToListAsync();
            var usersInfo = users.ConvertAll<UserInfoDto>(user=>new UserInfoDto { Id = user.Id, UserName=user.UserName });  
            return usersInfo;
             
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id){
            return await _context.Users.FindAsync(id);
        }
    }
}