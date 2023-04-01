using Saturn.Enums.Company;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace Saturn.DomainModels.Company
{
    [Table("CompanyInfo")]
    public class CompanyInfo : FullAuditedEntity<Guid>
    {
        public string PanNumber { get; set; }
        public string CompanyName { get; set; }

        public DateTime? NdaSignDate { get; set; }

        public string StreetLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ProfessionalSummary { get; set; }
        public CompanyUserType CompanyUserType { get; set; }

        public string ProfessionalPhoto { get; set; }
        public string CompanyLogo { get; set; }
        public string BusinessBrief { get; set; }
        public string Website { get; set; }

        public bool IsPersonalInfoSubmitted { get; set; }

        public bool IsCompanyInfoSubmitted { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser UserFK { get; set; }

        public int? NatureOfBusinessId { get; set; }

        [ForeignKey("NatureOfBusinessId")]
        public virtual NatureOfBusiness NatureOfBusinessFK { get; set; }

        public int? PrimaryIndustryId { get; set; }

        [ForeignKey("PrimaryIndustryId")]
        public virtual PrimaryIndustry PrimaryIndustryFK { get; set; }

        public int? SecondaryIndustryId { get; set; }

        [ForeignKey("SecondaryIndustryId")]
        public virtual SecondaryIndustry SecondaryIndustryFK { get; set; }

        public int? PrimaryEndCustomerId { get; set; }

        [ForeignKey("PrimaryEndCustomerId")]
        public virtual PrimaryEndCustomer PrimaryEndCustomerFK { get; set; }


        public virtual ICollection<CompanyCompetencyMapping> CompanyCompetencyMappings { get; set; }

        public virtual ICollection<CompanyRevenue> CompanyRevenues { get; set; }
        public virtual ICollection<CompanyKeyProblem> CompanyKeyProblems { get; set; }
        public virtual ICollection<CompanyTopProduct> CompanyTopProducts { get; set; }
        public virtual ICollection<CompanyTopCustomer> CompanyTopCustomers { get; set; }
        public virtual ICollection<CompanyTopMarket> CompanyTopMarkets { get; set; }
        public virtual ICollection<CompanyTopCompetitor> CompanyTopCompetitors { get; set; }
        public virtual ICollection<CompanyTeamMember> CompanyTeamMembers { get; set; }
    }
}
