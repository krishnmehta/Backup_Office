using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Application.Dtos;

namespace Saturn.BusinessUser.Dtos
{
    public class CompanyTopMarketDto : EntityDto<int>
    {
        public string RegionName { get; set; }

        public List<CompanyTopMarketRevenueDto> CompanyTopMarketRevenues { get; set; }
    }
}