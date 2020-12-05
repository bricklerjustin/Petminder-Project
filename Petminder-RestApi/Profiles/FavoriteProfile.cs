using AutoMapper;
using Petminder_RestApi.Models;
using Petminder_RestApi.Dtos;

namespace Petminder_RestApi.Profiles
{
    public class FavoriteProfile : Profile
    {
        public ExerciseProfile()
        {
            //Source -> Target
            CreateMap<Favorite, FavoriteReadDto>();
            CreateMap<FavoriteReadDto, Favorite>();
            CreateMap<Favorite, FavoriteUpdateCreateBaseDto>();
            CreateMap<FavoriteUpdateCreateBaseDto, Favorite>();
        }
    }
}