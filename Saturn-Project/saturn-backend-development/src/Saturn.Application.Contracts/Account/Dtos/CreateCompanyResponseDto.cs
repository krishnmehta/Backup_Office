

namespace Saturn.Account.Dtos
{
    public class CreateCompanyResponseDto
    {
        public bool IsError { get; set; }

        public bool IsEmailExist { get; set; }

        public bool IsPhoneNumberExist { get; set; }
    }
}
