using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace Saturn.DomainModels.Company
{
    [Table("CompanyTopProductRevenue")]
    public class CompanyTopProductRevenue : FullAuditedEntity<int>
    {
        public int FinancialYear { get; set; }
        public double Revenue { get; set; }
        public int CompanyTopProductId { get; set; }

        [ForeignKey("CompanyTopProductId")]
        public virtual CompanyTopProduct CompanyTopProductFK { get; set; }
    }
}