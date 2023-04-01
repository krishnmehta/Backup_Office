using Saturn.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace Saturn.Account.Dtos
{
    public class ResetPasswordRequestDto
    {
        public Guid UserId { get; set; }

        [Required]
        [RegularExpression(CompanyConst.PasswordRegex)]
        public string Password { get; set; }

        public string ResetToken { get; set; }
    }
}
