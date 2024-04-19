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
    public static class AccountVerificationMapper
    {
        private static IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AccountVerificationMapperProfile>()));

        public static AccountVerificationDto ToDto(this AccountVerification entity)
        {
            return mapper.Map<AccountVerificationDto>(entity);
        }

        public static AccountVerification ToEntity(this AccountVerificationDto dto)
        {
            return mapper.Map<AccountVerification>(dto);
        }

        public static List<AccountVerificationDto> ToListDto(this List<AccountVerification> entity)
        {
            return mapper.Map<List<AccountVerification>, List<AccountVerificationDto>>(entity);
        }
    }

    public class AccountVerificationMapperProfile : Profile
    {
        public AccountVerificationMapperProfile()
        {
            CreateMap<AccountVerification, AccountVerificationDto>().ReverseMap();
        }
    }
}
