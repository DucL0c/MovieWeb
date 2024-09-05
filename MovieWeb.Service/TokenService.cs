using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieWeb.Model.MappingModels;
using MovieWeb.Model.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieWeb.Service
{

    public interface ITokenService
    {
        string CreateToken(SystemUser user);
    }
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly ISystemUserService _systemUserService;
        private readonly TimeSpan _expiryDuration = TimeSpan.FromHours(2);

        public TokenService(IConfiguration config, ISystemUserService systemUserService)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"] ?? ""));
            _systemUserService = systemUserService;
        }
        public string CreateToken(SystemUser user)
        {

            var claims = new List<Claim>
            {

                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)

            };

            //Thêm role
            var roles = _systemUserService.GetRoles(user.Id).Result;
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            //kí token
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_expiryDuration),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
