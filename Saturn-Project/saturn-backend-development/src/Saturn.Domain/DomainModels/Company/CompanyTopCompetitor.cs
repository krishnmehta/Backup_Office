using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace Saturn.DomainModels.Company
{
    [Table("CompanyTopCompetitor")]
    public class CompanyTopCompetitor : FullAuditedEntity<int>
    {
        public string CompetitorName { get; set; }
        public string CompetitorLocation { get; set; }
        public Guid CompanyInfoId { get; set; }

        [ForeignKey("CompanyInfoId")]
        public virtual CompanyInfo CompanyInfoFK { get; set; }

        public virtual ICollection<CompanyTopCompetitorRevenue> CompanyTopCompetitorRevenues { get; set; }
    }
}