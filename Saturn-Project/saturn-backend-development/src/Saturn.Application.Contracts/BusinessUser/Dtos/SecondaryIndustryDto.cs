using Volo.Abp.Application.Dtos;

namespace Saturn.BusinessUser.Dtos
{
    public class SecondaryIndustryDto : EntityDto<int>
    {
        public string SecondaryIndustryName { get; set; }

        public int PrimaryIndustryId { get; set; }
    }
}
