using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace Saturn.DomainModels.Company
{
    [Table("CompanyTeamMember")]
    public class CompanyTeamMember : FullAuditedEntity<int>
    {
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }

        public Guid CompanyInfoId { get; set; }

        [ForeignKey("CompanyInfoId")]
        public virtual CompanyInfo CompanyInfoFK { get; set; }
    }
}

