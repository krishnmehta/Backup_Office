using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Saturn.DomainModels.Company
{
    [Table("PrimaryIndustry")]
    public class PrimaryIndustry : FullAuditedEntity<int>
    {
        public string PrimaryIndustryName { get; set; }

        public ICollection<SecondaryIndustry> SecondaryIndustries { get; set; }
    }
}
