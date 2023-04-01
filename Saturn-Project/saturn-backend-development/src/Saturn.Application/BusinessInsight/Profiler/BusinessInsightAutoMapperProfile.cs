using AutoMapper;
using Saturn.BusinessInsight.Dtos;
using Saturn.DomainModels.BusinessInsight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saturn.BusinessInsight.Profiler
{
    public class BusinessInsightAutoMapperProfile : Profile
    {
        public BusinessInsightAutoMapperProfile()
        {
            CreateMap<Product, ProductViewDetailsDto>().ReverseMap();
            CreateMap<Product, ProductDataPointListDto>().ReverseMap();
            CreateMap<ProductDataUploadPoint, ProductDataUploadPointDto>().ReverseMap();
        }
    }
}
