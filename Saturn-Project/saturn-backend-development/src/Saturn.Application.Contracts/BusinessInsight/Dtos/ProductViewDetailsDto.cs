using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Saturn.BusinessInsight.Dtos
{
    public class ProductViewDetailsDto : EntityDto<int>
    {
        public string Name { get; set; }

        public string QuestionnaireDescription { get; set; }

        public string TypeformLink { get; set; }
    }
}
