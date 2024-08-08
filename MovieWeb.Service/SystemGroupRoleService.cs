using MovieWeb.Data.Repository;
using MovieWeb.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWeb.Service
{
    public interface ISystemGroupRoleService
    {
        Task<GroupRole> AddGroupRole(GroupRole entity);
        Task<GroupRole> DeleteGroupRole(int id);
        Task<GroupRole> UpdateGroupRole(GroupRole entity);
        Task<GroupRole> GetGroupRoleById(int id);
        Task<IEnumerable<GroupRole>> GetGroupRoleAll();
        Task<Boolean> CheckContain(int id);

    }
    public class SystemGroupRoleService : ISystemGroupRoleService
    {
        private readonly ISystemGroupRoleRepository _systemGroupRoleRepository;

        public SystemGroupRoleService(ISystemGroupRoleRepository systemGroupRoleRepository)
        {
            _systemGroupRoleRepository = systemGroupRoleRepository;
        }
        public async Task<GroupRole> AddGroupRole(GroupRole entity)
        {
            return await _systemGroupRoleRepository.AddASync(entity);
        }

        public async Task<bool> CheckContain(int id)
        {
            return await _systemGroupRoleRepository.CheckContainsAsync(x => x.Id == id);
        }

        public async Task<GroupRole> DeleteGroupRole(int id)
        {
            return await _systemGroupRoleRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<GroupRole>> GetGroupRoleAll()
        {
            return await _systemGroupRoleRepository.GetAllAsync();
        }

        public Task<GroupRole> GetGroupRoleById(int id)
        {
            return _systemGroupRoleRepository.GetByIdAsync(id);
        }

        public Task<GroupRole> UpdateGroupRole(GroupRole entity)
        {
            return _systemGroupRoleRepository.UpdateASync(entity);
        }
    }
}
