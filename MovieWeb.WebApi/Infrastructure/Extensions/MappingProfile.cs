using AutoMapper;
using MovieWeb.Model.Models;
using MovieWeb.Model.ViewModels;

namespace MovieWeb.WebApi.Infrastructure.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SystemRoleViewModel, SystemRole>();
            CreateMap<SystemGroupViewModel, SystemGroup>();
            CreateMap<SystemGroupRoleViewModel, GroupRole>();
            CreateMap<SystemUserViewModel, SystemUser>();
        }
    }
}
