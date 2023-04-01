using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;

namespace Saturn.DomainModels.Company
{
    [Table("CompanyCompetencyMappings")]
    public class CompanyCompetencyMapping : Entity<int>
    {
        public Guid CompanyInfoId { get; set; }

        [ForeignKey("CompanyInfoId")]
        public virtual CompanyInfo CompanyInfoFK { get; set; }

        public int CompetencyId { get; set; }

        [ForeignKey("CompetencyId")]
        public virtual Competency CompetencyFK { get; set; }
    }
}
