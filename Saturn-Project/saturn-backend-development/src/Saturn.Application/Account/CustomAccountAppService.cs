using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Saturn.Account.Dtos;
using Saturn.Constants;
using Saturn.DomainModels.Company;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Twilio;
using Twilio.Rest.Verify.V2.Service;
using Volo.Abp.Account;
using Volo.Abp.Account.Emailing;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Emailing;
using Volo.Abp.Identity;
using Volo.Abp.Settings;

namespace Saturn.Account
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IAccountAppService), typeof(AccountAppService), typeof(CustomAccountAppService))]
    public class CustomAccountAppService : AccountAppService
    {

        private readonly IRepository<CompanyInfo> _companyInfoRepository;
        private readonly IConfiguration _appConfiguration;
        private readonly IRepository<IdentityUser, Guid> _identityUserRepository;
        private readonly IEmailSender _emailSender;

        public CustomAccountAppService(IdentityUserManager userManager,
            IIdentityRoleRepository roleRepository,
            IAccountEmailer accountEmailer,
            IdentitySecurityLogManager identitySecurityLogManager,
            IOptions<IdentityOptions> identityOptions,
            IRepository<CompanyInfo> companyInfoRepository,
            IConfiguration appConfiguration,
            IRepository<IdentityUser, Guid> identityUserRepository,
            IEmailSender emailSender) :
            base(userManager, roleRepository, accountEmailer, identitySecurityLogManager, identityOptions)
        {
            _companyInfoRepository = companyInfoRepository;
            _appConfiguration = appConfiguration;
            _identityUserRepository = identityUserRepository;
            _emailSender = emailSender;
        }

        public async Task<CreateCompanyResponseDto> CreateCompanyAsync(CreateCompanyDto input)
        {
            IdentityUser existingUser = await _identityUserRepository.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(input.Email.ToLower()) || x.PhoneNumber.ToLower().Equals(input.PhoneNumber.ToLower()));
            if (existingUser != null)
            {
                CreateCompanyResponseDto responseDto = new CreateCompanyResponseDto();
                responseDto.IsError = true;
                if (existingUser.Email.Equals(input.Email, StringComparison.InvariantCultureIgnoreCase))
                {
                    responseDto.IsEmailExist = true;
                }
                if (existingUser.PhoneNumber.Equals(input.PhoneNumber, StringComparison.InvariantCultureIgnoreCase))
                {
                    responseDto.IsPhoneNumberExist = true;
                }
                return responseDto;
            }
            try
            {
                IdentityUser user = new IdentityUser(GuidGenerator.Create(), input.Email, input.Email, CurrentTenant.Id);
                user.Name = input.FirstName;
                user.Surname = input.LastName;
                await UserManager.CreateAsync(user, input.Password);
                await UserManager.SetPhoneNumberAsync(user, input.PhoneNumber);
                await UserManager.AddToRoleAsync(user, StaticRoles.Company);
                await CurrentUnitOfWork.SaveChangesAsync();

                CompanyInfo companyInfo = new CompanyInfo();
                companyInfo = ObjectMapper.Map(input, companyInfo);
                companyInfo.UserId = user.Id;

                await _companyInfoRepository.InsertAsync(companyInfo);
                await CurrentUnitOfWork.SaveChangesAsync();
                return new CreateCompanyResponseDto();
            }
            catch
            {
                return new CreateCompanyResponseDto()
                {
                    IsError = true,
                };
            }
        }

        public override async Task<IdentityUserDto> RegisterAsync(RegisterDto input)
        {
            throw new NotImplementedException();
        }

        public async Task SendPasswordResetLinkAsync(string email)
        {
            IdentityUser user = await GetUserByEmailAsync(email);
            string resetToken = await UserManager.GeneratePasswordResetTokenAsync(user);

            string url = $"{_appConfiguration["App:ClientUrl"]}/reset-password?token={HttpUtility.UrlEncode(resetToken)}&userId={user.Id}";

            await _emailSender.SendAsync(
                email,
                "Reset Password",
                url,
                false
            );
        }

        public async Task ResetPasswordByTokenAsync(ResetPasswordRequestDto input)
        {
            await base.ResetPasswordAsync(ObjectMapper.Map<ResetPasswordRequestDto, ResetPasswordDto>(input));
        }

        public async Task SendOtp(OtpSendRequest otpSendRequest)
        {
            string accountSid = _appConfiguration["Twilio:Account_Sid"];
            string authToken = _appConfiguration["Twilio:Auth_Token"];
            string serviceId = _appConfiguration["Twilio:Service_Id"];

            TwilioClient.Init(accountSid, authToken);

            VerificationResource verification = await VerificationResource.CreateAsync(
                to: otpSendRequest.PhoneNumber,
                channel: "sms",
                pathServiceSid: serviceId
            );
        }
        public async Task<bool> VerifyOtp(OtpVerifyRequest otpVerifyRequest)
        {
            string accountSid = _appConfiguration["Twilio:Account_Sid"];
            string authToken = _appConfiguration["Twilio:Auth_Token"];
            string serviceId = _appConfiguration["Twilio:Service_Id"];

            TwilioClient.Init(accountSid, authToken);
            try
            {
                VerificationCheckResource verificationCheck = await VerificationCheckResource.CreateAsync(
                    to: otpVerifyRequest.PhoneNumber,
                    code: otpVerifyRequest.Otp,
                    pathServiceSid: serviceId
                );
                if (verificationCheck.Status.Equals("approved", StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, ex.ToString());
                return false;
            }
        }
    }
}

