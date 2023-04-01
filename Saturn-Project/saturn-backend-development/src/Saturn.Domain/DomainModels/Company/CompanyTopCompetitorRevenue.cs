using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace Saturn.DomainModels.Company
{
    [Table("CompanyTopCompetitorRevenue")]
    public class CompanyTopCompetitorRevenue : FullAuditedEntity<int>
    {
        public int FinancialYear { get; set; }
        public double Revenue { get; set; }
        public int CompanyTopCompetitorId { get; set; }

        [ForeignKey("CompanyTopCompetitorId")]
        public virtual CompanyTopCompetitor CompanyTopCompetitorFK { get; set; }
    }
}