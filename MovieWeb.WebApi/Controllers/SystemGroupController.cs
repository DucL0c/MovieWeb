using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieWeb.Model.MappingModels;
using MovieWeb.Model.Models;
using MovieWeb.Model.ViewModels;
using MovieWeb.Service;
using MovieWeb.WebApi.Infrastructure.Core;
using System;
using System.Threading.Tasks;

namespace MovieWeb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SystemGroupController : ControllerBase
    {
        private readonly ISystemGroupService _systemGroupService;
        private readonly IMapper _mapper;

        public SystemGroupController(ISystemGroupService systemGroupService, IMapper mapper)
        {
            _systemGroupService = systemGroupService;
            _mapper = mapper;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> AddGroup(SystemGroupViewModel group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _systemGroupService.CheckGroupAsync(group.GroupCode))
            {
                return BadRequest("Group already exists.");
            }

            var model = _mapper.Map<SystemGroupViewModel, SystemGroup>(group);
            if(model == null)
            {
                return BadRequest("Model is null");
            }

            model.CreatedDate = DateTime.Now;
            model.CreatedBy = "Admin";

            try
            {
                var result = await _systemGroupService.AddGroup(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the group: {ex.Message}");
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateGroup(SystemGroupViewModel group)
        {
            if (group == null)
            {
                return BadRequest("Group data is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingGroup = await _systemGroupService.GetGroupById(group.Id);
            if (existingGroup == null)
            {
                return NotFound("Group not found.");
            }

            try
            {
                var model = _mapper.Map<SystemGroup>(group);
                model.ModifiedDate = DateTime.Now;
                model.ModifierBy = "Admin";
                if (model == null)
                {
                    return BadRequest("Model is null");
                }

                var result = await _systemGroupService.UpdateGroup(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the group: {ex.Message}");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid group ID.");
            }

            var existingGroup = await _systemGroupService.GetGroupById(id);
            if (existingGroup == null)
            {
                return NotFound("Group not found.");
            }

            try
            {
                var result = await _systemGroupService.DeleteGroup(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the group: {ex.Message}");
            }
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "ViewGroup")]
        public async Task<IActionResult> GetAllGroups()
        {
            try
            {
                var result = await _systemGroupService.GetAllGroup();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving all groups: {ex.Message}");
            }
        }

        [HttpGet("Getallbypaging")]
        public async Task<IActionResult> GetAllByPaging(int page = 0, int pageSize = 100, string? keyword = null)
        {
            try
            {
                var model = await _systemGroupService.GetAllGroup();
                int totalRow = 0;
                var data = model.OrderByDescending(x => x.Id).Skip(page * pageSize).Take(pageSize);
                var mapping = _mapper.Map<IEnumerable<SystemGroup>, IEnumerable<SystemGroupDto>>(data);

                totalRow = model.Count();

                var paging = new PaginationSet<SystemGroupDto>()
                {
                    Items = mapping,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                return Ok(paging);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetGroupById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid group ID.");
            }

            try
            {
                var result = await _systemGroupService.GetGroupById(id);
                if (result == null)
                {
                    return NotFound("Group not found.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the group: {ex.Message}");
            }
        }

        [HttpGet("CheckContain/{id}")]
        public async Task<IActionResult> CheckContain(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid group ID.");
            }

            try
            {
                var result = await _systemGroupService.CheckContainAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while checking group existence: {ex.Message}");
            }
        }
    }
}
