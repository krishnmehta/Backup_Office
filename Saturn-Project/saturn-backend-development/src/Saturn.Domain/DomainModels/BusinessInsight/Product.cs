using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Saturn.DomainModels.BusinessInsight
{
    [Table("Product")]
    public class Product : FullAuditedEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string QuestionnaireDescription { get; set; }

        public string TypeformLink { get; set; }
        public string DataUploadTypeformLink { get; set; }

        public bool IsMainProduct { get; set; }

        public virtual ICollection<ProductDataUploadPoint> ProductDataUploadPoints { get; set; }
    }
}
