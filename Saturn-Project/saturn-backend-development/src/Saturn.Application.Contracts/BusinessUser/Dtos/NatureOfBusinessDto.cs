using Volo.Abp.Application.Dtos;

namespace Saturn.BusinessUser.Dtos
{
    public class NatureOfBusinessDto : EntityDto<int>
    {
        public string BusinessActivity { get; set; }
    }
}
