using AutoMapper;
using SandBoxLib.Implemention.Services;
using SandBoxLib.Models.RRServices.users;
using UsersApp.Data.EntityModels;

namespace UsersApp.Config
{
    public class MappingEntity : Profile
    {
        public MappingEntity()
        {
            CreateMap<UsersCreateRequest, eUser>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => Utils.HashSHA512(src.Password)));

            CreateMap<eUser, UsersGetByIdResponse>();
        }
    }
}
