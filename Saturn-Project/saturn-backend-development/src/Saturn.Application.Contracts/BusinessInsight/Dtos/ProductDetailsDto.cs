using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Saturn.BusinessInsight.Dtos
{
    public class ProductDetailsDto : EntityDto<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
