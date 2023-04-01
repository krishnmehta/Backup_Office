using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Saturn.BusinessUser.Dtos
{
    public class CompanyTopProductDto : EntityDto<int>
    {
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }

        public List<CompanyTopProductRevenueDto> CompanyTopProductRevenues { get; set; }
    }
}