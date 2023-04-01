using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Saturn.DomainModels.Company
{
    [Table("SecondaryIndustry")]
    public class SecondaryIndustry : FullAuditedEntity<int>
    {
        public string SecondaryIndustryName { get; set; }

        public int PrimaryIndustryId { get; set; }

        [ForeignKey("PrimaryIndustryId")]
        public virtual PrimaryIndustry PrimaryIndustryFK { get; set; }
    }
}
