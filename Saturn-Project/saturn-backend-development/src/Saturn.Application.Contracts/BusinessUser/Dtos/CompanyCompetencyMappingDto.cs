
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Application.Dtos;

namespace Saturn.BusinessUser.Dtos
{
    public class CompanyCompetencyMappingDto : EntityDto<int>
    {
        public Guid CompanyInfoId { get; set; }

        public int CompetencyId { get; set; }
    }
}
