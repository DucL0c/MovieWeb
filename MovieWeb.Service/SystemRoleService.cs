using MovieWeb.Data.Repository;
using MovieWeb.Model.MappingModels;
using MovieWeb.Model.Models;

namespace MovieWeb.Service
{
    public interface ISystemRoleService
    {
        Task<SystemRole> Add(SystemRole systemRole);
        Task<SystemRole> Update(SystemRole systemRole);
        Task<SystemRole> Delete(int id);
        Task<SystemRole> GetById(int id);
        Task<IEnumerable<SystemRole>> GetAll();
        Task<Boolean> CheckContain(int id);
        Task<Boolean> CheckRole(string roleCode);
        Task<IQueryable<MenuDto>> GetTreeMenuByUserId(int userId);

    }
    public class SystemRoleService : ISystemRoleService
    {
        private readonly ISystemRoleRepository _systemRoleRepository;
        public SystemRoleService(ISystemRoleRepository systemRoleRepository)
        {
            _systemRoleRepository = systemRoleRepository;
        }
        public async Task<SystemRole> Add(SystemRole systemRole)
        {
            return await _systemRoleRepository.AddASync(systemRole);
        }

        public Task<bool> CheckContain(int id)
        {
            return _systemRoleRepository.CheckContainsAsync(x => x.Id == id);
        }

        public async Task<bool> CheckRole(string roleCode)
        {
            return await _systemRoleRepository.CheckContainsAsync(x => x.RoleCode == roleCode);
        }

        public async Task<SystemRole> Delete(int id)
        {
            return await _systemRoleRepository.DeleteAsync(id);
        }
        public async Task<IEnumerable<SystemRole>> GetAll()
        {
            return await _systemRoleRepository.GetAllAsync();
        }

        public async Task<SystemRole> GetById(int id)
        {
            return await _systemRoleRepository.GetByIdAsync(id);
        }

        public async Task<SystemRole> Update(SystemRole systemRole)
        {
            return await _systemRoleRepository.UpdateASync(systemRole);
        }
        public async Task<IQueryable<MenuDto>> GetTreeMenuByUserId(int userId)
        {
            return await _systemRoleRepository.GetTreeMenuByUserId(userId);
        }
    }
}
