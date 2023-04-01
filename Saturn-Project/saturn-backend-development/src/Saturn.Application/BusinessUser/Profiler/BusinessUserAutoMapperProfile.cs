using AutoMapper;
using Saturn.Account.Dtos;
using Saturn.BusinessUser.Dtos;
using Saturn.DomainModels.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace Saturn.BusinessUser.Profiler
{
    public class BusinessUserAutoMapperProfile : Profile
    {
        public BusinessUserAutoMapperProfile()
        {
            CreateMap<CompanyInfo, NdaDetailsDto>().ReverseMap();
            CreateMap<Competency, CompetencyDto>().ReverseMap();
            CreateMap<IdentityUser, PersonalInfoDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Surname))
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CompanyInfo, PersonalInfoDto>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CompanyCompetencyMapping, CompanyCompetencyMappingDto>().ReverseMap();

            CreateMap<CompanyInfo, CompanyInfoDto>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CompanyInfo, PersonalInfoDto>().ReverseMap();
            CreateMap<CompanyRevenue, CompanyRevenueDto>().ReverseMap();
            CreateMap<NatureOfBusiness, NatureOfBusinessDto>().ReverseMap();
            CreateMap<PrimaryIndustry, PrimaryIndustryDto>().ReverseMap();
            CreateMap<SecondaryIndustry, SecondaryIndustryDto>().ReverseMap();
            CreateMap<PrimaryEndCustomer, PrimaryEndCustomerDto>().ReverseMap();
            CreateMap<KeyProblem, KeyProblemDto>().ReverseMap();
            CreateMap<CompanyKeyProblem, CompanyKeyProblemDto>().ReverseMap();
            CreateMap<CompanyTopProduct, CompanyTopProductDto>().ReverseMap();
            CreateMap<CompanyTopProductRevenue, CompanyTopProductRevenueDto>().ReverseMap();
            CreateMap<CompanyTopCustomer, CompanyTopCustomerDto>().ReverseMap();
            CreateMap<CompanyTopCustomerRevenue, CompanyTopCustomerRevenueDto>().ReverseMap();
            CreateMap<CompanyTopMarket, CompanyTopMarketDto>().ReverseMap();
            CreateMap<CompanyTopMarketRevenue, CompanyTopMarketRevenueDto>().ReverseMap();
            CreateMap<CompanyTopCompetitor, CompanyTopCompetitorDto>().ReverseMap();
            CreateMap<CompanyTopCompetitorRevenue, CompanyTopCompetitorRevenueDto>().ReverseMap();
            CreateMap<CompanyTeamMember, CompanyTeamMemberDto>().ReverseMap();
        }
    }
}
