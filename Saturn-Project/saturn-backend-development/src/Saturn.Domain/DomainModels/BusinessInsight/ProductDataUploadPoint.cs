using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Saturn.DomainModels.BusinessInsight
{
    public class ProductDataUploadPoint : FullAuditedEntity<int>
    {
        public string DataPointName { get; set; }

        public string TemplateLink { get; set; }

    }
}
