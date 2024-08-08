using MovieWeb.Data.Repository;
using MovieWeb.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieWeb.Service
{
    public interface ISystemUserService
    {
        Task<SystemUser> AddASync(SystemUser entity);
        Task<SystemUser> UpdateASync(SystemUser entity);
        Task<SystemUser> DeleteASync(int id);
        Task<bool> CheckUserNameContainsAsync(string Username);
        Task<SystemUser> GetByIdAsync(int id);
        Task<SystemUser> GetByUserNameAsync(string Username);
        Task<IEnumerable<SystemUser>> GetAllAsync();
    }
    public class SystemUserService : ISystemUserService
    {
        private readonly ISystemUserRepository _systemUserRepository;

        public SystemUserService(ISystemUserRepository systemUserRepository)
        {
            _systemUserRepository = systemUserRepository;
        }
        public async Task<SystemUser> AddASync(SystemUser entity)
        {
            return await _systemUserRepository.AddASync(entity);
        }

        public async Task<bool> CheckUserNameContainsAsync(string Username)
        {
            return await _systemUserRepository.CheckContainsAsync(x => x.Username == Username);
        }

        public async Task<SystemUser> DeleteASync(int id)
        {
            return await _systemUserRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<SystemUser>> GetAllAsync()
        {
            return await _systemUserRepository.GetAllAsync();
        }

        public async Task<SystemUser> GetByIdAsync(int id)
        {
            return await _systemUserRepository.GetByIdAsync(id);
        }

        public async Task<SystemUser> GetByUserNameAsync(string Username)
        {
            return await _systemUserRepository.GetByUsernameAsync(Username);
        }

        public async Task<SystemUser> UpdateASync(SystemUser entity)
        {
            return await _systemUserRepository.UpdateASync(entity);
        }
    }
}
