using Volo.Abp.Application.Dtos;

namespace Saturn.BusinessUser.Dtos
{
    public class PrimaryIndustryDto : EntityDto<int>
    {
        public string PrimaryIndustryName { get; set; }
    }
}
