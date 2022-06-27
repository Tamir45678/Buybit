using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Users.Data;
using Users.DTOs;
using Users.Entities;
using Users.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Commands.Messages;
using NServiceBus;

namespace Users.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMessageSession _messageSession;
        private readonly ILogger _logger;
        public AccountController(DataContext context,ITokenService tokenService, ILogger<AccountController> logger, IMessageSession messageSession)
        {
            _tokenService = tokenService;
            _context = context;
            _logger = logger;
            _messageSession = messageSession;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto){
            
            if (registerDto.Username == null || registerDto.Password == null) return BadRequest("Username or password is missing");
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");
            
            using var hmac = new HMACSHA512();
            var user = new AppUser{
                UserName = registerDto.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };


            _context.Users.Add(user);
            await _context.SaveChangesAsync(true);
            var userDetails = await _context.Users.SingleOrDefaultAsync(x => x.UserName == user.UserName);
            await _messageSession.Publish(new UserCreated() { Id = userDetails.Id });
            return Created("User Added Successfully",user.UserName);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto){
            _logger.LogInformation($"username: {loginDto.Username}");
            if (loginDto.Password == null)
                    return Unauthorized("Invalid Passwword");
                var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

                if (user == null) return Unauthorized("Invalid Username");
                
                using var hmac = new HMACSHA512(user.PasswordSalt);

                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
                }

                 
                return new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Token = _tokenService.CreateToken(user)
                };
        }

        private async Task<bool> UserExists(string username){
            return await _context.Users.AnyAsync(x=>x.UserName==username);
        }
    }
}