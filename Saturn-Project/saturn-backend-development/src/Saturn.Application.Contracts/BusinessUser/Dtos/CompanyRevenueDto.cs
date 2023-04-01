using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Application.Dtos;

namespace Saturn.BusinessUser.Dtos
{
    public class CompanyRevenueDto : EntityDto<int>
    {
        public int FinancialYear { get; set; }
        public double Revenue { get; set; }
    }
}