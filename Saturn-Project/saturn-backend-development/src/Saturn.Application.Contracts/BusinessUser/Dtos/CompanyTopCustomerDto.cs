using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Application.Dtos;

namespace Saturn.BusinessUser.Dtos
{
    public class CompanyTopCustomerDto : EntityDto<int>
    {
        public string CustomerName { get; set; }
        public string CustomerCategory { get; set; }
        public List<CompanyTopCustomerRevenueDto> CompanyTopCustomerRevenues { get; set; }
    }
}