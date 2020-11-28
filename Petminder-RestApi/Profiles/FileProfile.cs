using AutoMapper;
using Petminder_RestApi.Dtos;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Profiles
{
        public class FileProfile : Profile
    {
        public FileProfile()
        {
            //Source -> Target
            CreateMap<Files, FileReadDto>();
            CreateMap<FileCreateDto, Filedata>();
            CreateMap<FileCreateDto, Files>();
            CreateMap<FileUpdateDto, Files>();
        }
    }
}