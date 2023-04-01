using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace Saturn.DomainModels.Company
{
    [Table("CompanyRevenue")]
    public class CompanyRevenue : FullAuditedEntity<int>
    {
        public int FinancialYear { get; set; }
        public double Revenue { get; set; }
        public Guid CompanyInfoId { get; set; }

        [ForeignKey("CompanyInfoId")]
        public virtual CompanyInfo CompanyInfoFK { get; set; }
    }
}