using MovieWeb.Data.Infrastructure;
using MovieWeb.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWeb.Data.Repository
{
    public interface ISystemGroupRoleRepository : IRepository<GroupRole>
    {

    }
    public class SystemGroupRoleRepository : RepositoryBase<GroupRole>, ISystemGroupRoleRepository
    {
        private readonly MovieContext _dbContext;
        public SystemGroupRoleRepository(MovieContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
