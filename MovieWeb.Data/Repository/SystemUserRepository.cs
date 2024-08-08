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
    }
}
