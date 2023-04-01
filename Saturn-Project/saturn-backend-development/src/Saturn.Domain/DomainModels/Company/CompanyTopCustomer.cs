using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace Saturn.DomainModels.Company
{
    [Table("CompanyTopCustomer")]
    public class CompanyTopCustomer : FullAuditedEntity<int>
    {
        public string CustomerName { get; set; }
        public string CustomerCategory { get; set; }
        public Guid CompanyInfoId { get; set; }

        [ForeignKey("CompanyInfoId")]
        public virtual CompanyInfo CompanyInfoFK { get; set; }
        public virtual ICollection<CompanyTopCustomerRevenue> CompanyTopCustomerRevenues { get; set; }
    }
}