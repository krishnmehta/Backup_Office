using Volo.Abp.Application.Dtos;

namespace Saturn.BusinessUser.Dtos
{
    public class CompanyTopProductRevenueDto : EntityDto<int>
    {
        public int FinancialYear { get; set; }
        public double Revenue { get; set; }
    }
}