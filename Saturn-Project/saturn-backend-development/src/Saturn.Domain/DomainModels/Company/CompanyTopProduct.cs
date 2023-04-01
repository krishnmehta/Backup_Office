using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace Saturn.DomainModels.Company
{
    [Table("CompanyTopProduct")]
    public class CompanyTopProduct : FullAuditedEntity<int>
    {
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public Guid CompanyInfoId { get; set; }

        [ForeignKey("CompanyInfoId")]
        public virtual CompanyInfo CompanyInfoFK { get; set; }

        public virtual ICollection<CompanyTopProductRevenue> CompanyTopProductRevenues { get; set; }
    }
}