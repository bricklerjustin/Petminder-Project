using AutoMapper;
using Petminder_RestApi.Models;
using Petminder_RestApi.Dtos;

namespace Petminder_RestApi.Profiles
{
    public class AuthenticateProfile : Profile
    {
        public AuthenticateProfile()
        {
            //Source -> Target
            CreateMap<Users, AuthenticateReadDto>();
            CreateMap<AuthenticateReadDto, Users>();
        }
    }
}