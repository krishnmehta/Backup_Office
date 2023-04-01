using AutoMapper;
using Saturn.Account.Dtos;
using Saturn.DomainModels.Company;
using Volo.Abp.Account;

namespace Saturn;

public class AccountAutoMapperProfile : Profile
{
    public AccountAutoMapperProfile()
    {
        CreateMap<CompanyInfo, CreateCompanyDto>().ReverseMap();
        CreateMap<ResetPasswordRequestDto, ResetPasswordDto>().ReverseMap();
    }
}
