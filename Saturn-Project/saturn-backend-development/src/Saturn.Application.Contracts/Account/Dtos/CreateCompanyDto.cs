using Saturn.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Saturn.Account.Dtos
{
    public class CreateCompanyDto
    {
        [Required]
        [StringLength(CompanyConst.MaxNameLength, MinimumLength = CompanyConst.MinNameLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(CompanyConst.MaxNameLength, MinimumLength = CompanyConst.MinNameLength)]
        public string LastName { get; set; }

        [Required]
        [StringLength(CompanyConst.MaxNameLength, MinimumLength = CompanyConst.MinNameLength)]
        public string CompanyName { get; set; }


        [Required]
        [RegularExpression(CompanyConst.EmailRegex)]
        public string Email { get; set; }


        [Required]
        [RegularExpression(CompanyConst.PhoneNumberRegex)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(CompanyConst.MaxPanLength, MinimumLength = CompanyConst.MinPanLength)]
        public string PanNumber { get; set; }

        [Required]
        [RegularExpression(CompanyConst.PasswordRegex)]
        public string Password { get; set; }
    }
}
