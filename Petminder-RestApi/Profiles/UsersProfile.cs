using AutoMapper;
using Petminder_RestApi.Dtos;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<Users, UserCreateDto>();
            CreateMap<Users, UserReadDto>();
            CreateMap<UserCreateDto, Users>();
            CreateMap<UserUpdateDto, Users>();
            CreateMap<Users, UserUpdateDto>();
        }
    }
}