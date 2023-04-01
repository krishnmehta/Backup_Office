using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Application.Dtos;

namespace Saturn.BusinessUser.Dtos
{
    public class CompanyKeyProblemDto : EntityDto<int>
    {
        public int KeyProblemId { get; set; }
        public string Problem { get; set; }
    }
}