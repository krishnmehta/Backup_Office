
namespace Saturn.Constants
{
    public static class CompanyConst
    {
        public const int MaxNameLength = 20;
        public const int MinNameLength = 1;

        public const int MaxPanLength = 15;
        public const int MinPanLength = 8;

        public const string PhoneNumberRegex = @"^\+?[0-9]{2}[0-9]{10}$";
        public const string EmailRegex = @"^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$";
        public const string PasswordRegex = @"(?=^.{8,15}$)(?=.*\d)(?=.*[@#%&!$*]+)(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";
    }
}
