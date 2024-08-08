using MovieWeb.Data.Infrastructure;
using MovieWeb.Model.Models;

namespace MovieWeb.Data.Repository
{
    public interface ISystemGroupRepository : IRepository<SystemGroup>
    {
    }
    public class SystemGroupRepository : RepositoryBase<SystemGroup>, ISystemGroupRepository
    {
        private readonly MovieContext _dbContext;
        public SystemGroupRepository(MovieContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
