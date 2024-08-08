using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieWeb.Model.MappingModels;
using MovieWeb.Model.Models;
using MovieWeb.Model.ViewModels;
using MovieWeb.Service;
using System.Security.Cryptography;
using System.Text;

namespace MovieWeb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemUserController : ControllerBase
    {
        private readonly ISystemUserService _systemUserService;
        private readonly ISystemGroupService _systemGroupService;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public SystemUserController(ISystemUserService systemUserService,
            ISystemGroupService systemGroupService, IMapper mapper,
            ITokenService tokenService)
        {
            _systemUserService = systemUserService;
            _systemGroupService = systemGroupService;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<SystemUserDto>> Create(SystemUserViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(await _systemUserService.CheckUserNameContainsAsync(viewModel.Username)){
                return BadRequest("Username already exists");
            }
            var groupExist = await _systemGroupService.CheckContainAsync(viewModel.SystemGroupid);
            if(!groupExist)
            {
                return BadRequest("Group not found");
            }

            var systemUser =  _mapper.Map<SystemUser>(viewModel);
            using var hmac = new HMACSHA512();
            systemUser.Username = viewModel.Username.ToLower();
            systemUser.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(viewModel.Password));
            systemUser.PasswordSalt = hmac.Key;
            systemUser.CreatedBy = "Admin";
            systemUser.CreatedDate = DateTime.Now;

            var result = await _systemUserService.AddASync(systemUser);
            return new SystemUserDto
            {
                Username = systemUser.Username,
                Token = _tokenService.CreateToken(systemUser)
            };
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _systemUserService.GetAllAsync();
            return Ok(result);
        }
    }
}
