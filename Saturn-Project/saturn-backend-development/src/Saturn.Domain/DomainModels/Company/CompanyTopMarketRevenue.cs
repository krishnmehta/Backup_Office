using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace Saturn.DomainModels.Company
{
    [Table("CompanyTopMarketRevenue")]
    public class CompanyTopMarketRevenue : FullAuditedEntity<int>
    {
        public int FinancialYear { get; set; }
        public double Revenue { get; set; }
        public int CompanyTopMarketId { get; set; }

        [ForeignKey("CompanyTopMarketId")]
        public virtual CompanyTopMarket CompanyTopMarketFK { get; set; }
    }
}