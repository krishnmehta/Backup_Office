using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace Saturn.DomainModels.Company
{
    [Table("CompanyTopMarket")]
    public class CompanyTopMarket : FullAuditedEntity<int>
    {
        public string RegionName { get; set; }
        public Guid CompanyInfoId { get; set; }

        [ForeignKey("CompanyInfoId")]
        public virtual CompanyInfo CompanyInfoFK { get; set; }

        public virtual ICollection<CompanyTopMarketRevenue> CompanyTopMarketRevenues { get; set; }
    }
}