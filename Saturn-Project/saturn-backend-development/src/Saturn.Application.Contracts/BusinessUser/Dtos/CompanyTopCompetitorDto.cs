using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Application.Dtos;

namespace Saturn.BusinessUser.Dtos
{
    public class CompanyTopCompetitorDto : EntityDto<int>
    {
        public string CompetitorName { get; set; }
        public string CompetitorLocation { get; set; }
        public List<CompanyTopCompetitorRevenueDto> CompanyTopCompetitorRevenues { get; set; }
    }
}