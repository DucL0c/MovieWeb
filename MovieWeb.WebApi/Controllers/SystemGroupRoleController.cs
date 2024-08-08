using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieWeb.Model.Models;
using MovieWeb.Model.ViewModels;
using MovieWeb.Service;
using System;
using System.Threading.Tasks;

namespace MovieWeb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemGroupRoleController : ControllerBase
    {
        private readonly ISystemGroupRoleService _systemGroupRoleService;
        private readonly ISystemGroupService _systemGroupService;
        private readonly ISystemRoleService _systemRoleService;
        private readonly IMapper _mapper;

        public SystemGroupRoleController(
            ISystemGroupRoleService systemGroupRoleService,
            ISystemGroupService systemGroupService,
            ISystemRoleService systemRoleService,
            IMapper mapper)
        {
            _systemGroupRoleService = systemGroupRoleService;
            _systemGroupService = systemGroupService;
            _systemRoleService = systemRoleService;
            _mapper = mapper;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(SystemGroupRoleViewModel groupRole)
        {
            if (groupRole == null) return BadRequest("Group role data is required.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                if (!await EntityExists(groupRole.SystemGroupid, groupRole.SystemRoleid))
                    return BadRequest("Group or Role does not exist.");

                var model = _mapper.Map<GroupRole>(groupRole);
                var result = await _systemGroupRoleService.AddGroupRole(model);

                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, SystemGroupRoleViewModel groupRole)
        {
            if (groupRole == null) return BadRequest("Group role data is required.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                if (!await EntityExists(groupRole.SystemGroupid, groupRole.SystemRoleid))
                    return BadRequest("Group or Role does not exist.");

                var model = _mapper.Map<GroupRole>(groupRole);
                var result = await _systemGroupRoleService.UpdateGroupRole(model);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var groupRole = await _systemGroupRoleService.GetGroupRoleById(id);
                if (groupRole == null) return NotFound("Group role not found.");

                await _systemGroupRoleService.DeleteGroupRole(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _systemGroupRoleService.GetGroupRoleAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("CheckContain/{id}")]
        public async Task<IActionResult> CheckContain(int id)
        {
            try
            {
                var result = await _systemGroupRoleService.CheckContain(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _systemGroupRoleService.GetGroupRoleById(id);
                if (result == null) return NotFound("Group role not found.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private async Task<bool> EntityExists(int groupId, int roleId)
        {
            var groupExists = await _systemGroupService.CheckContainAsync(groupId);
            var roleExists = await _systemRoleService.CheckContain(roleId);
            return groupExists && roleExists;
        }
    }
}
