using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Saturn.DomainModels.Company
{
    [Table("Competency")]
    public class Competency : FullAuditedEntity<int>
    {
        public string Title { get; set; }
    }
}
