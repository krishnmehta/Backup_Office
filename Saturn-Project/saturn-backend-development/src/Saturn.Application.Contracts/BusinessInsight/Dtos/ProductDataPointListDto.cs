using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Saturn.BusinessInsight.Dtos
{
    public class ProductDataPointListDto
    {
        public List<ProductDataUploadPointDto> ProductDataUploadPoints { get; set; }

        public string DataUploadTypeformLink { get; set; }
    }
}
