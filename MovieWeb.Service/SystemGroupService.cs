using MovieWeb.Data.Repository;
using MovieWeb.Model.Models;

namespace MovieWeb.Service
{
    public interface ISystemGroupService
    {
        Task<SystemGroup> AddGroup(SystemGroup group);
        Task<SystemGroup> UpdateGroup(SystemGroup group);
        Task<SystemGroup> DeleteGroup(int id);
        Task<SystemGroup> GetGroupById(int id);
        Task<IEnumerable<SystemGroup>> GetAllGroup();
        Task<Boolean> CheckContainAsync(int id);
        Task<Boolean> CheckGroupAsync(string groupCode);
    }
    public class SystemGroupService : ISystemGroupService
    {
        private readonly ISystemGroupRepository _systemGroupRepository;

        public SystemGroupService(ISystemGroupRepository systemGroupRepository)
        {
            _systemGroupRepository = systemGroupRepository;
        }
        public async Task<SystemGroup> AddGroup(SystemGroup group)
        {
            return await _systemGroupRepository.AddASync(group);
        }

        public async Task<bool> CheckContainAsync(int id)
        {
            return await _systemGroupRepository.CheckContainsAsync(x => x.Id == id);
        }

        public async Task<bool> CheckGroupAsync(string groupCode)
        {
            return await _systemGroupRepository.CheckContainsAsync(x => x.GroupCode == groupCode);
        }

        public async Task<SystemGroup> DeleteGroup(int id)
        {
            return await _systemGroupRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<SystemGroup>> GetAllGroup()
        {
            return await _systemGroupRepository.GetAllAsync(null!);
        }

        public async Task<SystemGroup> GetGroupById(int id)
        {
            return await _systemGroupRepository.GetByIdAsync(id);
        }

        public async Task<SystemGroup> UpdateGroup(SystemGroup group)
        {
            return await _systemGroupRepository.UpdateASync(group);
        }
    }
}
