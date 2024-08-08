using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieWeb.Model.MappingModels;
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

#pragma warning disable CS8604 // Possible null reference argument.
            using var hmac = new HMACSHA512(user.PasswordSalt);
#pragma warning restore CS8604 // Possible null reference argument.

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }

            return new SystemUserDto
            {
                Username = loginDto.Username,
                Token = _tokenService.CreateToken(user),
            };
        }
    }
}
