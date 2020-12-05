using AutoMapper;
using Petminder_RestApi.Models;
using Petminder_RestApi.Dtos;

namespace Petminder_RestApi.Profiles
{
    public class FavoriteProfile : Profile
    {
        public FavoriteProfile()
        {
            //Source -> Target
            CreateMap<Favorites, FavoriteReadDto>();
            CreateMap<FavoriteReadDto, Favorites>();
            CreateMap<Favorites, FavoriteUpdateCreateBaseDto>();
            CreateMap<FavoriteUpdateCreateBaseDto, Favorites>();
        }
    }
}