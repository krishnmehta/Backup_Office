﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Saturn.DomainModels.Company
{
    [Table("NatureOfBusiness")]
    public class NatureOfBusiness : FullAuditedEntity<int>
    {
        public string BusinessActivity { get; set; }
    }
}
