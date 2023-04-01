using Volo.Abp.Application.Dtos;

namespace Saturn.BusinessUser.Dtos
{
    public class KeyProblemDto : EntityDto<int>
    {
        public string Problem { get; set; }
    }
}
