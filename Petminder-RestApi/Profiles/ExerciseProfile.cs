using AutoMapper;
using Petminder_RestApi.Models;
using Petminder_RestApi.Dtos;
using System.Collections.Generic;

namespace Petminder_RestApi.Profiles
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            //Source -> Target
            CreateMap<Exercises, ExerciseReadDto>();
            CreateMap<ExerciseReadDto, Exercises>();
            CreateMap<ExerciseUpdateCreateBaseDto, Exercises>();
            CreateMap<Exercises, ExerciseUpdateCreateBaseDto>();
        }
    }
}