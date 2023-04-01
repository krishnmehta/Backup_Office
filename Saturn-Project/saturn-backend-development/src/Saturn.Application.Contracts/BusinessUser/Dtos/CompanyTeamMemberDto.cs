using Volo.Abp.Application.Dtos;

namespace Saturn.BusinessUser.Dtos
{

    public class CompanyTeamMemberDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
    }
}

