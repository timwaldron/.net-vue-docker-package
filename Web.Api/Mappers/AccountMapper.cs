using AutoMapper;
using Web.Api.Models;
using Web.Api.Models.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Mappers
{
    public static class AccountMapper
    {
        private static IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AccountMapperProfile>()));

        public static AccountDto ToDto(this Account entity)
        {
            return mapper.Map<AccountDto>(entity);
        }

        public static Account ToEntity(this AccountDto dto)
        {
            return mapper.Map<Account>(dto);
        }

        public static List<AccountDto> ToListDto(this List<Account> entity)
        {
            return mapper.Map<List<Account>, List<AccountDto>>(entity);
        }
    }

    public class AccountMapperProfile : Profile
    {
        public AccountMapperProfile()
        {
            CreateMap<Account, AccountDto>().ReverseMap();
        }
    }
}
