using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieWeb.Model.Models;
using MovieWeb.Model.ViewModels;
using MovieWeb.Service;
using System;
using System.Threading.Tasks;

namespace MovieWeb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SystemRoleController : ControllerBase
    {
        private readonly ISystemRoleService _systemRoleService;
        private readonly IMapper _autoMapper;

        public SystemRoleController(ISystemRoleService systemRoleService, IMapper autoMapper)
        {
            _systemRoleService = systemRoleService;
            _autoMapper = autoMapper;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> AddRole(SystemRoleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _systemRoleService.CheckRole(viewModel.RoleCode))
            {
                return BadRequest("Role already exists.");
            }

            var model = _autoMapper.Map<SystemRole>(viewModel);

            if (model == null)
            {
                return BadRequest("Model is null");
            }

            model.CreatedDate = DateTime.Now;
            model.CreatedBy = "Admin";

            try
            {
                var result = await _systemRoleService.Add(model);
                return Ok(result);
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "An error occurred while saving changes to the database.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var result = await _systemRoleService.GetAll();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred while fetching all roles.");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            try
            {
                var result = await _systemRoleService.GetById(id);
                if (result == null)
                {
                    return NotFound("Role not found.");
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred while fetching the role.");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                var model = await _systemRoleService.GetById(id);
                if (model == null)
                {
                    return NotFound("Role not found.");
                }

                var result = await _systemRoleService.Delete(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred while deleting the role.");
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateRole(SystemRoleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var model = _autoMapper.Map<SystemRole>(viewModel);
                model.ModifiedDate = DateTime.Now;
                model.ModifierBy = "Admin";
                if (model == null)
                {
                    return BadRequest("Model is null");
                }

                var result = await _systemRoleService.Update(model);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred while updating the role.");
            }
        }

        [HttpGet("CheckContain/{id}")]
        public async Task<IActionResult> CheckContain(int id)
        {
            try
            {
                var result = await _systemRoleService.CheckContain(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred while checking role existence.");
            }
        }

        [HttpGet("gettreeviewbyuser")]
        public async Task<IActionResult> GetTreeView(int userId)
        {
            try
            {
                //_logger.LogInformation("Run endpoint {endpoint} {verb}", "/api/aioaccesscontrol/appmenus/gettreeviewbyuser?userid={userId}", "GET");
                var query = await _systemRoleService.GetTreeMenuByUserId(userId);
                return Ok(query);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
