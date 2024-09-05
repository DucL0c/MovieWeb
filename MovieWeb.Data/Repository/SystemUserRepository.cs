using Microsoft.EntityFrameworkCore;
using MovieWeb.Data.Infrastructure;
using MovieWeb.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWeb.Data.Repository
{
    public interface ISystemUserRepository : IRepository<SystemUser>
    {
        Task<SystemUser> GetByUsernameAsync(string Username);
        Task<List<String>> GetRoles(int id);
    }
    public class SystemUserRepository : RepositoryBase<SystemUser>, ISystemUserRepository
    {
        private readonly MovieContext _dbContext;
        public SystemUserRepository(MovieContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SystemUser> GetByUsernameAsync(string Username)
        {
            return await Task.FromResult(_dbContext.SystemUsers.FirstOrDefault(x => x.Username == Username));
        }

        public async Task<List<string>> GetRoles(int id)
        {
            var query = await (from user in _dbContext.SystemUsers
                         join gr in _dbContext.SystemGroups on user.SystemGroupid equals gr.Id
                         join grro in _dbContext.GroupRoles on gr.Id equals grro.SystemGroupid
                         join role in _dbContext.SystemRoles on grro.SystemRoleid equals role.Id
                         where user.Id == id
                         select role.RoleCode).ToListAsync();
            return query;   
        }
    }
}
