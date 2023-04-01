using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Saturn.DomainModels.Company
{
    [Table("PrimaryEndCustomer")]
    public class PrimaryEndCustomer : FullAuditedEntity<int>
    {
        public string Customer { get; set; }
    }
}
