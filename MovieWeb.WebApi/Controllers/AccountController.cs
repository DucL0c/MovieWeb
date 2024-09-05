using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieWeb.Model.MappingModels;
using MovieWeb.Model.Models;
using MovieWeb.Service;
using System.Security.Cryptography;
using System.Text;

namespace MovieWeb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ISystemUserService _systemUserService;

        public AccountController(ITokenService tokenService,ISystemUserService systemUserService)
        {
            _tokenService = tokenService;
            _systemUserService = systemUserService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<SystemUserDto>> Login(LoginDto loginDto)
        {
            var user = await _systemUserService.GetByUserNameAsync(loginDto.Username);
            if (user == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }



            return new SystemUserDto
            {
                Id = user.Id,
                Username = loginDto.Username,
                Token =  "Bearer " + _tokenService.CreateToken(user),
                Email = user.Email,
                Name = user.Name,
                Role = _systemUserService.GetRoles(user.Id).Result.ToArray()
            };
        }
    }
}
