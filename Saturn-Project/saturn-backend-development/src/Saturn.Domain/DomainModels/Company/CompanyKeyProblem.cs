using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace Saturn.DomainModels.Company
{
    [Table("CompanyKeyProblem")]
    public class CompanyKeyProblem : FullAuditedEntity<int>
    {
        public Guid CompanyInfoId { get; set; }

        [ForeignKey("CompanyInfoId")]
        public virtual CompanyInfo CompanyInfoFK { get; set; }

        public int KeyProblemId { get; set; }

        [ForeignKey("KeyProblemId")]
        public virtual KeyProblem KeyProblemFK { get; set; }
    }
}