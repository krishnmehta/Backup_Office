﻿
namespace Saturn.Account.Dtos
{
    public class OtpVerifyRequest
    {
        public string PhoneNumber { get; set; }
        public string Otp { get; set; }
    }
}
