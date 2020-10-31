using AutoMapper;
using Petminder_RestApi.Dtos;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Profiles
{
    public class AccountsProfile : Profile
    {
        public AccountsProfile()
        {
            CreateMap<AccountCreateDto, Accounts>();
            CreateMap<Accounts, AccountReadDto>();
        }
        
    }
}