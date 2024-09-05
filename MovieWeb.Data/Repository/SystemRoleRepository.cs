using Microsoft.EntityFrameworkCore;
using MovieWeb.Data.Infrastructure;
using MovieWeb.Model.MappingModels;
using MovieWeb.Model.Models;

namespace MovieWeb.Data.Repository
{
    public interface ISystemRoleRepository : IRepository<SystemRole>
    {
        Task<IQueryable<MenuDto>> GetTreeMenuByUserId(int userId);
    }
    public class SystemRoleRepository : RepositoryBase<SystemRole>, ISystemRoleRepository
    {
        private readonly MovieContext _dbContext;
        public SystemRoleRepository(MovieContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<MenuDto>> GetTreeMenuByUserId(int userId)
        {
            var query = (await (from ro in _dbContext.SystemRoles
                                join gr in _dbContext.GroupRoles on ro.Id equals gr.SystemRoleid
                                join user in _dbContext.SystemUsers on gr.SystemGroupid equals user.SystemGroupid
                                where user.Id == userId && ro.ParentId == null
                                select new MenuDto
                                {
                                    Id = ro.Id,
                                    MenuName = ro.NameRole,
                                    ParentId = ro.ParentId,
                                    Icon = ro.Icon,
                                    Link = ro.Link,
                                    ActiveLink = ro.ActiveLink,
                                    Childrens = (from ro1 in _dbContext.SystemRoles.DefaultIfEmpty()
                                                 join gr1 in _dbContext.GroupRoles on ro1.Id equals gr1.SystemRoleid
                                                 join user1 in _dbContext.SystemUsers on gr1.SystemGroupid equals user1.SystemGroupid
                                                 where ro1.ParentId == ro.Id
                                                 select new MenuDto
                                                 {
                                                     Id = ro1.Id,
                                                     MenuName = ro1.NameRole,
                                                     ParentId = ro1.ParentId,
                                                     Icon = ro1.Icon,
                                                     Link = ro1.Link,
                                                     ActiveLink = ro1.ActiveLink
                                                 }
                                                  ).ToList()
                                }).ToListAsync()).AsQueryable();


            List<MenuDto> menus = new List<MenuDto>();
            foreach (var menu in query.ToList())
            {
                if (menu.Childrens?.Count > 0)
                {
                    menus.Add(menu);
                }
            }
            return menus.AsQueryable();
        }
    }
}
