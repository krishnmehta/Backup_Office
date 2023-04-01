using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace Saturn.DomainModels.Company
{
    [Table("CompanyTopCustomerRevenue")]
    public class CompanyTopCustomerRevenue : FullAuditedEntity<int>
    {
        public int FinancialYear { get; set; }
        public double Revenue { get; set; }
        public int CompanyTopCustomerId { get; set; }

        [ForeignKey("CompanyTopCustomerId")]
        public virtual CompanyTopCustomer CompanyTopCustomerFK { get; set; }
    }
}