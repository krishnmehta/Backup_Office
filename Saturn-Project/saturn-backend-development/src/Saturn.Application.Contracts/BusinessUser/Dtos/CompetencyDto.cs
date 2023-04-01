
using System;
using Volo.Abp.Application.Dtos;

namespace Saturn.BusinessUser.Dtos
{
    public class CompetencyDto : EntityDto<int>
    {
        public string Title { get; set; }
    }
}
