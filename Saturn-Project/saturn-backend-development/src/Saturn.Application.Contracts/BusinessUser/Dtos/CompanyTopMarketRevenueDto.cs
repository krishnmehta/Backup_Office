using Volo.Abp.Application.Dtos;

namespace Saturn.BusinessUser.Dtos
{
    public class CompanyTopMarketRevenueDto : EntityDto<int>
    {
        public int FinancialYear { get; set; }
        public double Revenue { get; set; }
        public int CompanyTopMarketId { get; set; }
    }
}