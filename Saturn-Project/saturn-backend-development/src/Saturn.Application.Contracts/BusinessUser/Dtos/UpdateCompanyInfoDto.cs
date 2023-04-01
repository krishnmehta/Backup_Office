
using System.Collections.Generic;
namespace Saturn.BusinessUser.Dtos
{
    public class UpdateCompanyInfoDto
    {
        public string BusinessBrief { get; set; }
        public string Website { get; set; }

        public string CompanyLogoName { get; set; }

        public int NatureOfBusinessId { get; set; }
        public int PrimaryIndustryId { get; set; }
        public int SecondaryIndustryId { get; set; }
        public int PrimaryEndCustomerId { get; set; }

        public List<CompanyRevenueDto> CompanyRevenues { get; set; }
        public List<CompanyKeyProblemDto> CompanyKeyProblems { get; set; }
        public List<CompanyTopProductDto> CompanyTopProducts { get; set; }
        public List<CompanyTopCustomerDto> CompanyTopCustomers { get; set; }
        public List<CompanyTopMarketDto> CompanyTopMarkets { get; set; }
        public List<CompanyTopCompetitorDto> CompanyTopCompetitors { get; set; }
        public List<CompanyTeamMemberDto> CompanyTeamMembers { get; set; }
    }
}
