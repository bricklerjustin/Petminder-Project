using AutoMapper;
using Petminder_RestApi.Models;
using Petminder_RestApi.Dtos;

namespace Petminder_RestApi.Profiles
{
    public class PetsProfile : Profile
    {
        public PetsProfile()
        {
            CreateMap<Pets, PetReadDto>();
        }
    }
}