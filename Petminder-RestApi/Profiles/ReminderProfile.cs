using AutoMapper;
using Petminder_RestApi.Models;
using Petminder_RestApi.Dtos;

namespace Petminder_RestApi.Profiles
{
    public class ReminderProfile : Profile
    {
        public ReminderProfile()
        {
            //Source -> Target
            CreateMap<Reminders, ReminderReadDto>();
            CreateMap<RemidnerCreateDto, Reminders>();
            CreateMap<ReminderUpdateDto, Reminders>();
            CreateMap<Reminders, ReminderUpdateDto>();
        }
    }
}