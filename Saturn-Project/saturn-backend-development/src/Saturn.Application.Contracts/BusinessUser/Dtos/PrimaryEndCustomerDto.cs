using Volo.Abp.Application.Dtos;

namespace Saturn.BusinessUser.Dtos
{
    public class PrimaryEndCustomerDto : EntityDto<int>
    {
        public string Customer { get; set; }
    }
}
