using Saturn.Constants;
using Saturn.Enums.Company;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Saturn.BusinessUser.Dtos
{
    public class UpdatePersonalInfoDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ProfessionalSummary { get; set; }
        public CompanyUserType CompanyUserType { get; set; }

        public string ProfessionalPhotoName { get; set; }

        public ICollection<int> CompetencyIds { get; set; }
    }
}
