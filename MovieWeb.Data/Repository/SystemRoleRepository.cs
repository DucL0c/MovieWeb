using MovieWeb.Data.Infrastructure;
using MovieWeb.Model.Models;

namespace MovieWeb.Data.Repository
{
    public interface ISystemRoleRepository : IRepository<SystemRole>
    {
    }
    public class SystemRoleRepository : RepositoryBase<SystemRole>, ISystemRoleRepository
    {
        private readonly MovieContext _dbContext;
        public SystemRoleRepository(MovieContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
