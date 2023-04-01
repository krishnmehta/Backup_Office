using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Saturn.Account;
using Saturn.Account.Dtos;
using Saturn.BusinessUser.Dtos;
using Saturn.DomainModels.Company;
using Saturn.Managers;
using Saturn.Managers.Company;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;


namespace Saturn.BusinessUser
{
    [Authorize]
    public class BusinessUserAppService : ApplicationService, IBusinessUserAppService
    {
        private readonly IRepository<CompanyInfo> _companyInfoRepository;
        private readonly IRepository<Competency> _competencyRepository;
        private readonly IRepository<CompanyCompetencyMapping> _companyCompetencyMappingRepository;
        private readonly IRepository<IdentityUser, Guid> _identityUserRepository;
        private readonly IdentityUserManager _userManager;
        private readonly IAmazonS3 _amazonS3;
        private readonly AwsCredentials _awsCreds;
        private readonly MicrosoftFormDetails _microsoftFormDetails;

        private readonly IRepository<CompanyRevenue> _companyRevenueRepository;
        private readonly IRepository<NatureOfBusiness> _natureOfBusinessRepository;
        private readonly IRepository<PrimaryIndustry> _primaryIndustryRepository;
        private readonly IRepository<SecondaryIndustry> _secondaryIndustryRepository;
        private readonly IRepository<PrimaryEndCustomer> _primaryEndCustomerRepository;
        private readonly IRepository<KeyProblem> _keyProblemRepository;
        private readonly IRepository<CompanyKeyProblem> _companyKeyProblemRepository;
        private readonly IRepository<CompanyTopProduct> _companyTopProductRepository;
        private readonly IRepository<CompanyTopProductRevenue> _companyTopProductRevenueRepository;
        private readonly IRepository<CompanyTopCustomer> _companyTopCustomerRepository;
        private readonly IRepository<CompanyTopCustomerRevenue> _companyTopCustomerRevenueRepository;
        private readonly IRepository<CompanyTopMarket> _companyTopMarketRepository;
        private readonly IRepository<CompanyTopMarketRevenue> _companyTopMarketRevenueRepository;
        private readonly IRepository<CompanyTopCompetitor> _companyTopCompetitorRepository;
        private readonly IRepository<CompanyTopCompetitorRevenue> _companyTopCompetitorRevenueRepository;
        private readonly IRepository<CompanyTeamMember> _companyTeamMemberRepository;

        private readonly CustomerTopProductManager _customerTopProductManager;
        private readonly CompanyInfoManager _companyInfoManager;

        public BusinessUserAppService(IRepository<CompanyInfo> companyInfoRepository,
            IRepository<IdentityUser, Guid> identityUserRepository,
            IRepository<Competency> competencyRepository,
            IdentityUserManager userManager,
            IRepository<CompanyCompetencyMapping> companyCompetencyMappingRepository,
            IAmazonS3 amazonS3,
            IOptionsSnapshot<AwsCredentials> awsCreds,
            IOptionsSnapshot<MicrosoftFormDetails> microsoftFormDetails,
            IRepository<CompanyRevenue> companyRevenueRepository,
            IRepository<NatureOfBusiness> natureOfBusinessRepository,
            IRepository<PrimaryIndustry> primaryIndustryRepository,
            IRepository<SecondaryIndustry> secondaryIndustryRepository,
            IRepository<PrimaryEndCustomer> primaryEndCustomerRepository,
            IRepository<KeyProblem> keyProblemRepository,
            IRepository<CompanyKeyProblem> companyKeyProblemRepository,
            IRepository<CompanyTopProduct> companyTopProductRepository,
            IRepository<CompanyTopProductRevenue> companyTopProductRevenueRepository,
            IRepository<CompanyTopCustomer> companyTopCustomerRepository,
            IRepository<CompanyTopCustomerRevenue> companyTopCustomerRevenueRepository,
            IRepository<CompanyTopMarket> companyTopMarketRepository,
            IRepository<CompanyTopMarketRevenue> companyTopMarketRevenueRepository,
            IRepository<CompanyTopCompetitor> companyTopCompetitorRepository,
            IRepository<CompanyTopCompetitorRevenue> companyTopCompetitorRevenueRepository,
            IRepository<CompanyTeamMember> companyTeamMemberRepository,
            CustomerTopProductManager customerTopProductManager,
            CompanyInfoManager companyInfoManager
            )
        {
            _companyInfoRepository = companyInfoRepository;
            _identityUserRepository = identityUserRepository;
            _competencyRepository = competencyRepository;
            _userManager = userManager;
            _companyCompetencyMappingRepository = companyCompetencyMappingRepository;
            _amazonS3 = amazonS3;
            _awsCreds = awsCreds.Value;
            _microsoftFormDetails = microsoftFormDetails.Value;
            _companyRevenueRepository = companyRevenueRepository;
            _natureOfBusinessRepository = natureOfBusinessRepository;
            _primaryIndustryRepository = primaryIndustryRepository;
            _secondaryIndustryRepository = secondaryIndustryRepository;
            _primaryEndCustomerRepository = primaryEndCustomerRepository;
            _keyProblemRepository = keyProblemRepository;
            _companyKeyProblemRepository = companyKeyProblemRepository;
            _companyTopProductRepository = companyTopProductRepository;
            _companyTopProductRevenueRepository = companyTopProductRevenueRepository;
            _companyTopCustomerRepository = companyTopCustomerRepository;
            _companyTopCustomerRevenueRepository = companyTopCustomerRevenueRepository;
            _companyTopMarketRepository = companyTopMarketRepository;
            _companyTopMarketRevenueRepository = companyTopMarketRevenueRepository;
            _companyTopCompetitorRepository = companyTopCompetitorRepository;
            _companyTopCompetitorRevenueRepository = companyTopCompetitorRevenueRepository;
            _companyTeamMemberRepository = companyTeamMemberRepository;

            _customerTopProductManager = customerTopProductManager;
            _companyInfoManager = companyInfoManager;
        }

        /// <summary>
        /// This method is used to get details for NDA sign page
        /// </summary>
        /// <returns>NdaDetailsDto object that holds details for NDA</returns>
        public async Task<NdaDetailsDto> GetNdaDetailsAsync()
        {
            NdaDetailsDto ndaDetailsDto = ObjectMapper.Map<CompanyInfo, NdaDetailsDto>(await _companyInfoRepository.FirstOrDefaultAsync(x => x.UserId == CurrentUser.Id));
            ndaDetailsDto.Name = $"{CurrentUser.Name} {CurrentUser.SurName}";
            return ndaDetailsDto;
        }

        /// <summary>
        /// This method is used to sign the NDA
        /// </summary>
        /// <returns>Task</returns>
        public async Task SignNdaAsync()
        {
            CompanyInfo companyInfo = await _companyInfoRepository.FirstOrDefaultAsync(x => x.UserId == CurrentUser.Id);
            if (companyInfo.NdaSignDate == null)
            {
                companyInfo.NdaSignDate = DateTime.Now;
            }
            await _companyInfoRepository.UpdateAsync(companyInfo);
        }

        /// <summary>
        /// This method is used to get the status of user onboarding
        /// </summary>
        /// <returns>OnboardingDto object that holds status of user onboarding</returns>
        public async Task<OnboardingDto> GetOnboardingStatus()
        {
            CompanyInfo companyInfo = await _companyInfoRepository.GetAsync(x => x.UserId == CurrentUser.Id);
            OnboardingDto onboardingDto = new OnboardingDto();
            onboardingDto.EngagementStatus = (await _companyInfoRepository.FirstAsync(x => x.UserId == CurrentUser.Id)).NdaSignDate != null;
            onboardingDto.PersonalInfoStatus = companyInfo.IsPersonalInfoSubmitted;
            onboardingDto.CompanyInfoStatus = companyInfo.IsCompanyInfoSubmitted;
            return onboardingDto;
        }

        /// <summary>
        /// This method is used to get all competencies
        /// </summary>
        /// <returns>List of competencydto object</returns>
        public async Task<List<CompetencyDto>> GetAllCompetencyAsync()
        {
            return ObjectMapper.Map<List<Competency>, List<CompetencyDto>>(await _competencyRepository.GetListAsync());
        }

        /// <summary>
        /// This method is used to get personal info.
        /// </summary>
        /// <returns>Returns PersonalInfoDto that holds personal info</returns>
        public async Task<PersonalInfoDto> GetPersonalInfoAsync()
        {
            IdentityUser currentUser = await _userManager.GetByIdAsync(CurrentUser.Id.Value);
            CompanyInfo companyInfo = await _companyInfoRepository.GetAsync(x => x.UserId == CurrentUser.Id);
            List<CompanyCompetencyMappingDto> companyCompetencyMappings = ObjectMapper.Map<List<CompanyCompetencyMapping>, List<CompanyCompetencyMappingDto>>(await _companyCompetencyMappingRepository.GetListAsync(x => x.CompanyInfoId == companyInfo.Id));

            PersonalInfoDto personalInfoDto = new PersonalInfoDto();

            personalInfoDto = ObjectMapper.Map<CompanyInfo, PersonalInfoDto>(companyInfo);
            personalInfoDto.CompanyCompetencyMappings = companyCompetencyMappings;
            personalInfoDto.FirstName = currentUser.Name;
            personalInfoDto.LastName = currentUser.Surname;
            personalInfoDto.Email = currentUser.Email;
            personalInfoDto.PhoneNumber = currentUser.PhoneNumber;

            if (!personalInfoDto.ProfessionalPhoto.IsNullOrWhiteSpace())
            {
                GetPreSignedUrlRequest getPreSignedUrlRequest = new GetPreSignedUrlRequest()
                {
                    BucketName = _awsCreds.BucketName,
                    Key = personalInfoDto.ProfessionalPhoto,
                    Expires = DateTime.UtcNow.AddMinutes(10)
                };

                personalInfoDto.ProfessionalPhotoUrl = _amazonS3.GetPreSignedURL(getPreSignedUrlRequest);
            }

            return personalInfoDto;
        }

        /// <summary>
        /// This method is used to save personal info.
        /// </summary>
        /// <returns>Task</returns>
        public async Task UpdatePersonalInfoAsync(UpdatePersonalInfoDto personalInfo)
        {

            IdentityUser currentUser = await _userManager.GetByIdAsync(CurrentUser.Id.Value);
            currentUser.Name = personalInfo.FirstName;
            currentUser.Surname = personalInfo.LastName;
            currentUser.SetPhoneNumber(personalInfo.PhoneNumber, true);
            await _userManager.UpdateAsync(currentUser);

            string emailToken = await _userManager.GenerateChangeEmailTokenAsync(currentUser, personalInfo.Email);
            await _userManager.ChangeEmailAsync(currentUser, personalInfo.Email, emailToken);

            CompanyInfo companyInfo = await _companyInfoRepository.GetAsync(x => x.UserId == CurrentUser.Id);
            List<CompanyCompetencyMapping> existingCompanyCompetencyMappings = await _companyCompetencyMappingRepository.GetListAsync(x => x.CompanyInfoId == companyInfo.Id);

            companyInfo.StreetLine = personalInfo.StreetLine;
            companyInfo.City = personalInfo.City;
            companyInfo.State = personalInfo.State;
            companyInfo.ProfessionalSummary = personalInfo.ProfessionalSummary;
            companyInfo.CompanyUserType = personalInfo.CompanyUserType;

            if (!personalInfo.ProfessionalPhotoName.IsNullOrWhiteSpace())
            {
                companyInfo.ProfessionalPhoto = personalInfo.ProfessionalPhotoName;
            }
            await _companyInfoRepository.UpdateAsync(companyInfo);

            List<CompanyCompetencyMapping> mappingsToDelete = new List<CompanyCompetencyMapping>();
            List<CompanyCompetencyMapping> mappingsToInsert = new List<CompanyCompetencyMapping>();

            if (personalInfo.CompetencyIds.IsNullOrEmpty())
            {
                mappingsToDelete = new List<CompanyCompetencyMapping>(existingCompanyCompetencyMappings);
            }
            else
            {
                foreach (var compatencyId in personalInfo.CompetencyIds)
                {
                    if (!existingCompanyCompetencyMappings.Any(x => x.CompanyInfoId == companyInfo.Id && x.CompetencyId == compatencyId))
                    {
                        mappingsToInsert.Add(new CompanyCompetencyMapping() { CompanyInfoId = companyInfo.Id, CompetencyId = compatencyId });
                    }
                }

                foreach (var mapping in existingCompanyCompetencyMappings)
                {
                    if (!personalInfo.CompetencyIds.Any(x => x == mapping.CompetencyId))
                    {
                        mappingsToDelete.Add(mapping);
                    }
                }
            }

            if (!mappingsToDelete.IsNullOrEmpty())
            {
                await _companyCompetencyMappingRepository.DeleteManyAsync(mappingsToDelete);
            }

            if (!mappingsToInsert.IsNullOrEmpty())
            {
                await _companyCompetencyMappingRepository.InsertManyAsync(mappingsToInsert);
            }
        }

        /// <summary>
        /// This method is used to upload professional photo
        /// </summary>
        /// <param name="personalInfoForm">Object that holds personal info details</param>
        /// <returns>Return UploadImageResponse object that contains the key of uploaded image</returns>
        public async Task<UploadImageResponse> UploadProfessionalPhoto([FromForm] UpdateProfessionalPhotoDto personalInfoForm)
        {
            try
            {
                UploadImageResponse imageResponse = new UploadImageResponse();
                if (personalInfoForm.ProfessionalPhoto != null)
                {
                    imageResponse.ImageName = await FileUpload(personalInfoForm.ProfessionalPhoto);
                }
                return imageResponse;
            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
                return null;
            }
        }

        /// <summary>
        /// This method is used to get company info
        /// </summary>
        /// <returns>Returns the object that holds company info details</returns>
        public async Task<CompanyInfoDto> GetCompanyInfoAsync()
        {
            IdentityUser currentUser = await _userManager.GetByIdAsync(CurrentUser.Id.Value);
            CompanyInfo companyInfo = await _companyInfoManager.GetCompanyInfoIncludeCompanyProfileDetailsByUserIdAsync(currentUser.Id);
            List<CompanyCompetencyMappingDto> companyCompetencyMappings = ObjectMapper.Map<List<CompanyCompetencyMapping>, List<CompanyCompetencyMappingDto>>(await _companyCompetencyMappingRepository.GetListAsync(x => x.CompanyInfoId == companyInfo.Id));
            CompanyInfoDto companyInfoDto = ObjectMapper.Map<CompanyInfo, CompanyInfoDto>(companyInfo);

            if (!companyInfoDto.CompanyLogo.IsNullOrWhiteSpace())
            {
                GetPreSignedUrlRequest getPreSignedUrlRequest = new GetPreSignedUrlRequest()
                {
                    BucketName = _awsCreds.BucketName,
                    Key = companyInfoDto.CompanyLogo,
                    Expires = DateTime.UtcNow.AddMinutes(10)
                };

                companyInfoDto.CompanyLogoUrl = _amazonS3.GetPreSignedURL(getPreSignedUrlRequest);
            }

            return companyInfoDto;
        }

        /// <summary>
        /// This method is used to upload company logo
        /// </summary>
        /// <param name="input">Object that holds logo</param>
        /// <returns>Return UploadImageResponse object that contains the key of uploaded image</returns>
        public async Task<UploadImageResponse> UploadCompanyLogo([FromForm] UploadCompanyLogoDto input)
        {
            try
            {
                UploadImageResponse imageResponse = new UploadImageResponse();
                if (input.CompanyLogo != null)
                {
                    imageResponse.ImageName = await FileUpload(input.CompanyLogo);
                }
                return imageResponse;
            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
                return null;
            }
        }

        /// <summary>
        /// This method is used to update company info
        /// </summary>
        /// <param name="updateCompanyInfoObj">Object that holds company info details to update</param>
        /// <param name="companyLogoName">Name of company logo</param>
        /// <returns>Task</returns>
        public async Task UpdateCompanyInfoAsync(UpdateCompanyInfoDto updateCompanyInfoObj)
        {
            try
            {
                CompanyInfo companyInfo = await _companyInfoManager.GetCompanyInfoIncludeCompanyProfileDetailsByUserIdAsync(CurrentUser.Id.Value);
                List<CompanyRevenue> existingCompanyRevenues = new List<CompanyRevenue>(companyInfo.CompanyRevenues);
                List<CompanyKeyProblem> existingCompanyKeyProblems = new List<CompanyKeyProblem>(companyInfo.CompanyKeyProblems);
                List<CompanyTopProduct> existingCompanyTopProducts = new List<CompanyTopProduct>(companyInfo.CompanyTopProducts);
                List<CompanyTopCustomer> existingCompanyTopCustomers = new List<CompanyTopCustomer>(companyInfo.CompanyTopCustomers);
                List<CompanyTopMarket> existingCompanyTopMarkets = new List<CompanyTopMarket>(companyInfo.CompanyTopMarkets);
                List<CompanyTopCompetitor> existingCompanyTopCompetitors = new List<CompanyTopCompetitor>(companyInfo.CompanyTopCompetitors);
                List<CompanyTeamMember> existingCompanyTeamMembers = new List<CompanyTeamMember>(companyInfo.CompanyTeamMembers);

                if (updateCompanyInfoObj.CompanyLogoName.IsNullOrWhiteSpace())
                {
                    companyInfo.CompanyLogo = updateCompanyInfoObj.CompanyLogoName;
                }

                companyInfo.BusinessBrief = updateCompanyInfoObj.BusinessBrief;
                companyInfo.Website = updateCompanyInfoObj.Website;
                companyInfo.NatureOfBusinessId = updateCompanyInfoObj.NatureOfBusinessId;
                companyInfo.PrimaryIndustryId = updateCompanyInfoObj.PrimaryIndustryId;
                companyInfo.SecondaryIndustryId = updateCompanyInfoObj.SecondaryIndustryId;
                companyInfo.PrimaryEndCustomerId = updateCompanyInfoObj.PrimaryEndCustomerId;

                await _companyInfoRepository.UpdateAsync(companyInfo);

                #region Company Revenue Insert/Update/Delete
                List<CompanyRevenue> companyRevenuesToInsert = new List<CompanyRevenue>();
                List<CompanyRevenue> companyRevenuesToDelete = new List<CompanyRevenue>();
                List<CompanyRevenue> companyRevenuesToUpdate = new List<CompanyRevenue>();

                // If in updateCompanyInfoObj.CompanyRevenues is null/empty means all existing company revenues need to deleted.
                if (updateCompanyInfoObj.CompanyRevenues.IsNullOrEmpty())
                {
                    companyRevenuesToDelete = new List<CompanyRevenue>(existingCompanyRevenues);
                }
                else
                {
                    // if existingCompanyRevenues is null/empty means all updateCompanyInfoObj.CompanyRevenues need to be inserted.
                    if (existingCompanyRevenues.IsNullOrEmpty())
                    {
                        foreach (var companyRevenue in updateCompanyInfoObj.CompanyRevenues)
                        {
                            companyRevenuesToInsert.Add(new CompanyRevenue() { CompanyInfoId = companyInfo.Id, FinancialYear = companyRevenue.FinancialYear, Revenue = companyRevenue.Revenue });
                        }
                    }
                    else
                    {
                        // Check existing company revenues whether they need to updated/deleted
                        foreach (var existingCompanyRevenue in existingCompanyRevenues)
                        {
                            // Find existingCompanyRevenue in updateCompanyInfoObj.CompanyRevenues
                            CompanyRevenueDto companyRevenue = updateCompanyInfoObj.CompanyRevenues.FirstOrDefault(x => x.FinancialYear == existingCompanyRevenue.FinancialYear);

                            // If existingCompanyRevenue found in updateCompanyInfoObj.CompanyRevenues then update existingCompanyRevenue.
                            if (companyRevenue != null)
                            {
                                existingCompanyRevenue.Revenue = companyRevenue.Revenue;
                                companyRevenuesToUpdate.Add(existingCompanyRevenue);
                            }
                            // If existingCompanyRevenue not found in updateCompanyInfoObj.CompanyRevenues then delete existingCompanyRevenue.
                            else
                            {
                                companyRevenuesToDelete.Add(existingCompanyRevenue);
                            }
                        }

                        // Checking updateCompanyInfoObj.CompanyRevenues whether they need to be added or not. 
                        foreach (var companyRevenue in updateCompanyInfoObj.CompanyRevenues)
                        {
                            // If exstingCompanyRevenue don't have revenue data of updateCompanyInfoObj.CompanyRevenues then add.
                            if (!existingCompanyRevenues.Any(x => x.FinancialYear == companyRevenue.FinancialYear))
                            {
                                companyRevenuesToInsert.Add(new CompanyRevenue() { CompanyInfoId = companyInfo.Id, FinancialYear = companyRevenue.FinancialYear, Revenue = companyRevenue.Revenue });
                            }
                        }
                    }
                }

                if (!companyRevenuesToInsert.IsNullOrEmpty())
                {
                    await _companyRevenueRepository.InsertManyAsync(companyRevenuesToInsert);
                }
                if (!companyRevenuesToUpdate.IsNullOrEmpty())
                {
                    await _companyRevenueRepository.UpdateManyAsync(companyRevenuesToUpdate);
                }
                if (!companyRevenuesToDelete.IsNullOrEmpty())
                {
                    await _companyRevenueRepository.DeleteManyAsync(companyRevenuesToDelete);
                }
                #endregion

                #region Key Problem Faced insert/Delete
                List<CompanyKeyProblem> companyKeyProblemsToInsert = new List<CompanyKeyProblem>();
                List<CompanyKeyProblem> companyKeyProblemsToDelete = new List<CompanyKeyProblem>();
                // If in updateCompanyInfoObj.CompanyKeyProblems is null/empty means all existing company key problems need to deleted.
                if (updateCompanyInfoObj.CompanyKeyProblems.IsNullOrEmpty())
                {
                    companyKeyProblemsToDelete.AddRange(existingCompanyKeyProblems);
                }
                else
                {
                    // If existingCompanyKeyProblems is null/empty then all updateCompanyInfoObj.CompanyKeyProblems need to be inserted
                    if (existingCompanyKeyProblems.IsNullOrEmpty())
                    {
                        foreach (var companyKeyProblem in updateCompanyInfoObj.CompanyKeyProblems)
                        {
                            // Insert new key problem in system if not exist.
                            if (companyKeyProblem.Id == 0 && !companyKeyProblem.Problem.IsNullOrWhiteSpace())
                            {
                                KeyProblem keyProblem = new KeyProblem() { Problem = companyKeyProblem.Problem };
                                await _keyProblemRepository.InsertAsync(keyProblem);
                                await CurrentUnitOfWork.SaveChangesAsync();
                                companyKeyProblem.Id = keyProblem.Id;
                            }
                            companyKeyProblemsToInsert.Add(new CompanyKeyProblem() { KeyProblemId = companyKeyProblem.Id, CompanyInfoId = companyInfo.Id });
                        }
                    }
                    else
                    {
                        foreach (var companyKeyProblem in updateCompanyInfoObj.CompanyKeyProblems)
                        {
                            // Insert new key problem in system if not exist.
                            if (companyKeyProblem.Id == 0 && !companyKeyProblem.Problem.IsNullOrWhiteSpace())
                            {
                                KeyProblem keyProblem = new KeyProblem() { Problem = companyKeyProblem.Problem };
                                await _keyProblemRepository.InsertAsync(keyProblem);
                                await CurrentUnitOfWork.SaveChangesAsync();
                                companyKeyProblem.KeyProblemId = keyProblem.Id;
                            }
                        }

                        // Check existingCompanyKeyProblems whether they need to be deleted or not.
                        foreach (var existingCompanyKeyProblem in existingCompanyKeyProblems)
                        {
                            // If existingCompanyKeyProblem not found in updateCompanyInfoObj.CompanyKeyProblems then delete existingCompanyKeyProblem.
                            if (!updateCompanyInfoObj.CompanyKeyProblems.Any(x => x.KeyProblemId == existingCompanyKeyProblem.KeyProblemId))
                            {
                                companyKeyProblemsToDelete.Add(existingCompanyKeyProblem);
                            }
                        }

                        // Check whether updateCompanyInfoObj.CompanyKeyProblems need to be added or not.
                        foreach (var companyKeyProblem in updateCompanyInfoObj.CompanyKeyProblems)
                        {
                            if (!existingCompanyKeyProblems.Any(x => x.KeyProblemId == companyKeyProblem.KeyProblemId))
                            {
                                companyKeyProblemsToInsert.Add(new CompanyKeyProblem() { KeyProblemId = companyKeyProblem.KeyProblemId, CompanyInfoId = companyInfo.Id });
                            }
                        }
                    }
                }

                if (!companyKeyProblemsToDelete.IsNullOrEmpty())
                {
                    await _companyKeyProblemRepository.DeleteManyAsync(companyKeyProblemsToDelete);
                }
                if (!companyKeyProblemsToInsert.IsNullOrEmpty())
                {
                    await _companyKeyProblemRepository.InsertManyAsync(companyKeyProblemsToInsert);
                }
                #endregion

                await UpdateCompanyTopProductsAsync(existingCompanyTopProducts, updateCompanyInfoObj.CompanyTopProducts, companyInfo.Id);
                await UpdateCompanyTopCustomersAsync(existingCompanyTopCustomers, updateCompanyInfoObj.CompanyTopCustomers, companyInfo.Id);
                await UpdateCompanyTopMarketsAsync(existingCompanyTopMarkets, updateCompanyInfoObj.CompanyTopMarkets, companyInfo.Id);
                await UpdateCompanyTopCompetitorsAsync(existingCompanyTopCompetitors, updateCompanyInfoObj.CompanyTopCompetitors, companyInfo.Id);
                await UpdateCompanyTeamMembersAsync(existingCompanyTeamMembers, updateCompanyInfoObj.CompanyTeamMembers, companyInfo.Id);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch(Exception e)
            {
                Logger.LogError(e.ToString());
            }
        }

        /// <summary>
        /// This method is used to get all nature of businesses.
        /// </summary>
        /// <returns>List of nature of businesses</returns>
        public async Task<List<NatureOfBusinessDto>> GetAllNatureOfBusinessesAsync()
        {
            return ObjectMapper.Map<List<NatureOfBusiness>, List<NatureOfBusinessDto>>(await _natureOfBusinessRepository.GetListAsync());
        }

        /// <summary>
        /// This method is used to get all primary industries.
        /// </summary>
        /// <returns>List of primary industries</returns>
        public async Task<List<PrimaryIndustryDto>> GetAllPrimaryIndustriesAsync()
        {
            return ObjectMapper.Map<List<PrimaryIndustry>, List<PrimaryIndustryDto>>(await _primaryIndustryRepository.GetListAsync());
        }

        /// <summary>
        /// This method is used to get all secondary industries.
        /// </summary>
        /// <returns>List of secondary industries</returns>
        public async Task<List<SecondaryIndustryDto>> GetAllSecondaryIndustriesAsync()
        {
            return ObjectMapper.Map<List<SecondaryIndustry>, List<SecondaryIndustryDto>>(await _secondaryIndustryRepository.GetListAsync());
        }

        /// <summary>
        /// This method is used to get all primary end customers.
        /// </summary>
        /// <returns>List of primary end customers</returns>
        public async Task<List<PrimaryEndCustomerDto>> GetAllPrimaryEndCustomersAsync()
        {
            return ObjectMapper.Map<List<PrimaryEndCustomer>, List<PrimaryEndCustomerDto>>(await _primaryEndCustomerRepository.GetListAsync());
        }

        /// <summary>
        /// This method is used to get all key problems.
        /// </summary>
        /// <returns>List of key problems</returns>
        public async Task<List<KeyProblemDto>> GetAllKeyProblemsAsync()
        {
            return ObjectMapper.Map<List<KeyProblem>, List<KeyProblemDto>>(await _keyProblemRepository.GetListAsync());
        }

        /// <summary>
        /// This method is used to get company info form link
        /// </summary>
        /// <returns>Form link object that contains link of form</returns>
        public Formlink GetCompanyInfoFormLinkAsync()
        {
            return new Formlink()
            {
                Link = _microsoftFormDetails.CompanyInfoForm
            };
        }

        /// <summary>
        /// This method is used to get personal info form link
        /// </summary>
        /// <returns>Form link object that contains link of form</returns>
        public Formlink GetPersonalInfoFormLinkAsync()
        {
            return new Formlink()
            {
                Link = _microsoftFormDetails.PersonalInfoForm
            };
        }

        /// <summary>
        /// This method is used to set personal info status as submitted
        /// </summary>
        /// <returns>Task</returns>
        public async Task SetPersonalInfoStatusSubmittedAsync()
        {
            CompanyInfo companyInfo = await _companyInfoRepository.GetAsync(x => x.UserId == CurrentUser.Id);
            companyInfo.IsPersonalInfoSubmitted = true;
            await _companyInfoRepository.UpdateAsync(companyInfo);
        }

        /// <summary>
        /// This method is used to set company info status as submitted
        /// </summary>
        /// <returns>Task</returns>
        public async Task SetCompanyInfoStatusSubmittedAsync()
        {
            CompanyInfo companyInfo = await _companyInfoRepository.GetAsync(x => x.UserId == CurrentUser.Id);
            companyInfo.IsCompanyInfoSubmitted = true;
            await _companyInfoRepository.UpdateAsync(companyInfo);
        }

        #region Private Methods

        /// <summary>
        /// This method is used to upload file to AWS S3 bucket
        /// </summary>
        /// <param name="formFile">File to be uploaded</param>
        /// <returns>Key of uploaded file</returns>
        private async Task<string> FileUpload(IFormFile formFile)
        {
            string key = $"{formFile.FileName}_{GuidGenerator.Create()}";
            PutObjectRequest PutObjectRequest = new PutObjectRequest()
            {
                BucketName = _awsCreds.BucketName,
                Key = key,
                InputStream = formFile.OpenReadStream()
            };

            PutObjectRequest.Metadata.Add("Content-Type", formFile.ContentType);
            await _amazonS3.PutObjectAsync(PutObjectRequest);
            return key;
        }

        private async Task UpdateCompanyTopProductsAsync(List<CompanyTopProduct> existingCompanyTopProducts, List<CompanyTopProductDto> updateCompanyTopProducts, Guid companyInfoId)
        {
            List<CompanyTopProduct> companyTopProductsToInsert = new List<CompanyTopProduct>();
            List<CompanyTopProduct> companyTopProductsToDelete = new List<CompanyTopProduct>();
            List<CompanyTopProduct> companyTopProductsToUpdate = new List<CompanyTopProduct>();
            List<CompanyTopProductRevenue> companyTopProductRevenuesToInsert = new List<CompanyTopProductRevenue>();
            List<CompanyTopProductRevenue> companyTopProductRevenuesToDelete = new List<CompanyTopProductRevenue>();

            // If in updateCompanyTopProducts is null/empty means all existingCompanyTopProduct need to deleted.
            if (updateCompanyTopProducts.IsNullOrEmpty())
            {
                companyTopProductsToDelete = new List<CompanyTopProduct>(existingCompanyTopProducts);
            }
            else
            {
                // if existingCompanyTopProducts is null/empty means all updateCompanyTopProducts need to be inserted.
                if (existingCompanyTopProducts.IsNullOrEmpty())
                {
                    foreach (var companyTopProduct in updateCompanyTopProducts)
                    {
                        CompanyTopProduct companyTopProductToAdd = new CompanyTopProduct()
                        {
                            CompanyInfoId = companyInfoId,
                            ProductName = companyTopProduct.ProductName,
                            ProductCategory = companyTopProduct.ProductCategory,
                            CompanyTopProductRevenues = new List<CompanyTopProductRevenue>()
                        };

                        if (!companyTopProduct.CompanyTopProductRevenues.IsNullOrEmpty())
                        {
                            foreach (var productRevenue in companyTopProduct.CompanyTopProductRevenues)
                            {
                                companyTopProductToAdd.CompanyTopProductRevenues.Add(new CompanyTopProductRevenue() { FinancialYear = productRevenue.FinancialYear, Revenue = productRevenue.Revenue });
                            }
                        }

                        companyTopProductsToInsert.Add(companyTopProductToAdd);
                    }
                }
                else
                {
                    // Check existingCompanyTopProduct whether they need to updated/deleted
                    foreach (var existingCompanyTopProduct in existingCompanyTopProducts)
                    {
                        // Find existingCompanyTopProduct in updateCompanyTopProducts
                        CompanyTopProductDto updateCompanyTopProduct = updateCompanyTopProducts.FirstOrDefault(x => x.Id == existingCompanyTopProduct.Id);

                        // If existingCompanyTopProduct found in updateCompanyTopProducts then update existingCompanyTopProduct.
                        if (updateCompanyTopProduct != null)
                        {
                            List<CompanyTopProductRevenue> existingCompanyTopProductRevenues = new List<CompanyTopProductRevenue>(existingCompanyTopProduct.CompanyTopProductRevenues);
                            existingCompanyTopProduct.ProductName = updateCompanyTopProduct.ProductName;
                            existingCompanyTopProduct.ProductCategory = updateCompanyTopProduct.ProductCategory;
                            companyTopProductsToUpdate.Add(existingCompanyTopProduct);

                            // If companyTopProduct.CompanyTopProductRevenues is null/empty then delete all product revenunes
                            if (updateCompanyTopProduct.CompanyTopProductRevenues.IsNullOrEmpty())
                            {
                                if (!existingCompanyTopProduct.CompanyTopProductRevenues.IsNullOrEmpty())
                                {
                                    companyTopProductRevenuesToDelete.AddRange(existingCompanyTopProduct.CompanyTopProductRevenues);
                                }
                            }
                            else
                            {
                                // If existingCompanyTopProduct don't have any revenues then add all companyTopProduct's revenues.
                                if (existingCompanyTopProduct.CompanyTopProductRevenues.IsNullOrEmpty())
                                {
                                    foreach (var productRevenue in updateCompanyTopProduct.CompanyTopProductRevenues)
                                    {
                                        companyTopProductRevenuesToInsert.Add(new CompanyTopProductRevenue() { CompanyTopProductId = existingCompanyTopProduct.Id, FinancialYear = productRevenue.FinancialYear, Revenue = productRevenue.Revenue });
                                    }
                                }
                                // If existingCompanyTopProduct has any revenues then decide whether to insert/update/delete
                                else
                                {
                                    foreach (var existingCompanyTopProductRevenue in existingCompanyTopProduct.CompanyTopProductRevenues)
                                    {
                                        CompanyTopProductRevenueDto companyTopProductRevenue = updateCompanyTopProduct.CompanyTopProductRevenues.FirstOrDefault(x => x.FinancialYear == existingCompanyTopProductRevenue.FinancialYear);

                                        // If existingCompanyTopProductRevenue found in updateCompanyTopProduct.CompanyTopProductRevenues then update existingCompanyTopProductRevenue. 
                                        if (companyTopProductRevenue != null)
                                        {
                                            existingCompanyTopProductRevenue.Revenue = companyTopProductRevenue.Revenue;
                                        }
                                        // If existingCompanyTopProductRevenue not found in updateCompanyTopProduct.CompanyTopProductRevenues then delete existingCompanyTopProductRevenue. 
                                        else
                                        {
                                            companyTopProductRevenuesToDelete.Add(existingCompanyTopProductRevenue);
                                        }
                                    }

                                    foreach (var companyTopProductRevenue in updateCompanyTopProduct.CompanyTopProductRevenues)
                                    {
                                        if (!existingCompanyTopProduct.CompanyTopProductRevenues.Any(x => x.FinancialYear == companyTopProductRevenue.FinancialYear))
                                        {
                                            companyTopProductRevenuesToInsert.Add(new CompanyTopProductRevenue() { CompanyTopProductId = existingCompanyTopProduct.Id, FinancialYear = companyTopProductRevenue.FinancialYear, Revenue = companyTopProductRevenue.Revenue });
                                        }
                                    }
                                }
                            }
                        }
                        // If existingCompanyTopProduct not found in updateCompanyTopProducts then delete existingCompanyTopProduct.
                        else
                        {
                            companyTopProductsToDelete.Add(existingCompanyTopProduct);
                        }
                    }

                    // Check updateCompanyTopProducts whether they need to added or not
                    foreach (var updateCompanyTopProduct in updateCompanyTopProducts)
                    {
                        // Check if existingCompanyTopProducts has any product data of updateCompanyTopProduct and if not found inser that product
                        if (!existingCompanyTopProducts.Any(x => x.Id == updateCompanyTopProduct.Id))
                        {
                            CompanyTopProduct companyTopProductToAdd = new CompanyTopProduct()
                            {
                                CompanyInfoId = companyInfoId,
                                ProductName = updateCompanyTopProduct.ProductName,
                                ProductCategory = updateCompanyTopProduct.ProductCategory,
                                CompanyTopProductRevenues = new List<CompanyTopProductRevenue>()
                            };

                            if (!updateCompanyTopProduct.CompanyTopProductRevenues.IsNullOrEmpty())
                            {
                                foreach (var productRevenue in updateCompanyTopProduct.CompanyTopProductRevenues)
                                {
                                    companyTopProductToAdd.CompanyTopProductRevenues.Add(new CompanyTopProductRevenue() { FinancialYear = productRevenue.FinancialYear, Revenue = productRevenue.Revenue });
                                }
                            }

                            companyTopProductsToInsert.Add(companyTopProductToAdd);
                        }
                    }
                }
            }

            if (!companyTopProductsToInsert.IsNullOrEmpty())
            {
                await _companyTopProductRepository.InsertManyAsync(companyTopProductsToInsert);
            }
            if (!companyTopProductsToUpdate.IsNullOrEmpty())
            {
                await _companyTopProductRepository.UpdateManyAsync(companyTopProductsToUpdate);
            }
            if (!companyTopProductRevenuesToInsert.IsNullOrEmpty())
            {
                await _companyTopProductRevenueRepository.InsertManyAsync(companyTopProductRevenuesToInsert);
            }
            if (!companyTopProductRevenuesToDelete.IsNullOrEmpty())
            {
                await _companyTopProductRevenueRepository.DeleteManyAsync(companyTopProductRevenuesToDelete);
            }
            if (!companyTopProductsToDelete.IsNullOrEmpty())
            {
                await _companyTopProductRepository.DeleteManyAsync(companyTopProductsToDelete);
            }
        }

        private async Task UpdateCompanyTopCustomersAsync(List<CompanyTopCustomer> existingCompanyTopCustomers, List<CompanyTopCustomerDto> updateCompanyTopCustomers, Guid companyInfoId)
        {
            List<CompanyTopCustomer> companyTopCustomersToInsert = new List<CompanyTopCustomer>();
            List<CompanyTopCustomer> companyTopCustomersToDelete = new List<CompanyTopCustomer>();
            List<CompanyTopCustomer> companyTopCustomersToUpdate = new List<CompanyTopCustomer>();
            List<CompanyTopCustomerRevenue> companyTopCustomerRevenuesToInsert = new List<CompanyTopCustomerRevenue>();
            List<CompanyTopCustomerRevenue> companyTopCustomerRevenuesToDelete = new List<CompanyTopCustomerRevenue>();

            // If in updateCompanyTopCustomers is null/empty means all existingCompanyTopCustomer need to deleted.
            if (updateCompanyTopCustomers.IsNullOrEmpty())
            {
                companyTopCustomersToDelete = new List<CompanyTopCustomer>(existingCompanyTopCustomers);
            }
            else
            {
                // if existingCompanyTopCustomers is null/empty means all updateCompanyTopCustomers need to be inserted.
                if (existingCompanyTopCustomers.IsNullOrEmpty())
                {
                    foreach (var companyTopCustomer in updateCompanyTopCustomers)
                    {
                        CompanyTopCustomer companyTopCustomerToAdd = new CompanyTopCustomer()
                        {
                            CompanyInfoId = companyInfoId,
                            CustomerName = companyTopCustomer.CustomerName,
                            CustomerCategory = companyTopCustomer.CustomerCategory,
                            CompanyTopCustomerRevenues = new List<CompanyTopCustomerRevenue>()
                        };

                        if (!companyTopCustomer.CompanyTopCustomerRevenues.IsNullOrEmpty())
                        {
                            foreach (var customerRevenue in companyTopCustomer.CompanyTopCustomerRevenues)
                            {
                                companyTopCustomerToAdd.CompanyTopCustomerRevenues.Add(new CompanyTopCustomerRevenue() { FinancialYear = customerRevenue.FinancialYear, Revenue = customerRevenue.Revenue });
                            }
                        }

                        companyTopCustomersToInsert.Add(companyTopCustomerToAdd);
                    }
                }
                else
                {
                    // Check existingCompanyTopCustomer whether they need to updated/deleted
                    foreach (var existingCompanyTopCustomer in existingCompanyTopCustomers)
                    {
                        // Find existingCompanyTopCustomer in updateCompanyTopCustomers
                        CompanyTopCustomerDto updateCompanyTopCustomer = updateCompanyTopCustomers.FirstOrDefault(x => x.Id == existingCompanyTopCustomer.Id);

                        // If existingCompanyTopCustomer found in updateCompanyTopCustomers then update existingCompanyTopCustomer.
                        if (updateCompanyTopCustomer != null)
                        {
                            List<CompanyTopCustomerRevenue> existingCompanyTopCustomerRevenues = new List<CompanyTopCustomerRevenue>(existingCompanyTopCustomer.CompanyTopCustomerRevenues);
                            existingCompanyTopCustomer.CustomerName = updateCompanyTopCustomer.CustomerName;
                            existingCompanyTopCustomer.CustomerCategory = updateCompanyTopCustomer.CustomerCategory;
                            companyTopCustomersToUpdate.Add(existingCompanyTopCustomer);

                            // If companyTopCustomer.CompanyTopCustomerRevenues is null/empty then delete all customer revenunes
                            if (updateCompanyTopCustomer.CompanyTopCustomerRevenues.IsNullOrEmpty())
                            {
                                if (!existingCompanyTopCustomer.CompanyTopCustomerRevenues.IsNullOrEmpty())
                                {
                                    companyTopCustomerRevenuesToDelete.AddRange(existingCompanyTopCustomer.CompanyTopCustomerRevenues);
                                }
                            }
                            else
                            {
                                // If existingCompanyTopCustomer don't have any revenues then add all companyTopCustomer's revenues.
                                if (existingCompanyTopCustomer.CompanyTopCustomerRevenues.IsNullOrEmpty())
                                {
                                    foreach (var customerRevenue in updateCompanyTopCustomer.CompanyTopCustomerRevenues)
                                    {
                                        companyTopCustomerRevenuesToInsert.Add(new CompanyTopCustomerRevenue() { CompanyTopCustomerId = existingCompanyTopCustomer.Id, FinancialYear = customerRevenue.FinancialYear, Revenue = customerRevenue.Revenue });
                                    }
                                }
                                // If existingCompanyTopCustomer has any revenues then decide whether to insert/update/delete
                                else
                                {
                                    foreach (var existingCompanyTopCustomerRevenue in existingCompanyTopCustomer.CompanyTopCustomerRevenues)
                                    {
                                        CompanyTopCustomerRevenueDto companyTopCustomerRevenue = updateCompanyTopCustomer.CompanyTopCustomerRevenues.FirstOrDefault(x => x.FinancialYear == existingCompanyTopCustomerRevenue.FinancialYear);

                                        // If existingCompanyTopCustomerRevenue found in updateCompanyTopCustomer.CompanyTopCustomerRevenues then update existingCompanyTopCustomerRevenue. 
                                        if (companyTopCustomerRevenue != null)
                                        {
                                            existingCompanyTopCustomerRevenue.Revenue = companyTopCustomerRevenue.Revenue;
                                        }
                                        // If existingCompanyTopCustomerRevenue not found in updateCompanyTopCustomer.CompanyTopCustomerRevenues then delete existingCompanyTopCustomerRevenue. 
                                        else
                                        {
                                            companyTopCustomerRevenuesToDelete.Add(existingCompanyTopCustomerRevenue);
                                        }
                                    }

                                    foreach (var companyTopCustomerRevenue in updateCompanyTopCustomer.CompanyTopCustomerRevenues)
                                    {
                                        if (!existingCompanyTopCustomer.CompanyTopCustomerRevenues.Any(x => x.FinancialYear == companyTopCustomerRevenue.FinancialYear))
                                        {
                                            companyTopCustomerRevenuesToInsert.Add(new CompanyTopCustomerRevenue() { CompanyTopCustomerId = existingCompanyTopCustomer.Id, FinancialYear = companyTopCustomerRevenue.FinancialYear, Revenue = companyTopCustomerRevenue.Revenue });
                                        }
                                    }
                                }
                            }
                        }
                        // If existingCompanyTopCustomer not found in updateCompanyTopCustomers then delete existingCompanyTopCustomer.
                        else
                        {
                            companyTopCustomersToDelete.Add(existingCompanyTopCustomer);
                        }
                    }

                    // Check updateCompanyTopCustomers whether they need to added or not
                    foreach (var updateCompanyTopCustomer in updateCompanyTopCustomers)
                    {
                        // Check if existingCompanyTopCustomers has any customer data of updateCompanyTopCustomer and if not found inser that customer
                        if (!existingCompanyTopCustomers.Any(x => x.Id == updateCompanyTopCustomer.Id))
                        {
                            CompanyTopCustomer companyTopCustomerToAdd = new CompanyTopCustomer()
                            {
                                CompanyInfoId = companyInfoId,
                                CustomerName = updateCompanyTopCustomer.CustomerName,
                                CustomerCategory = updateCompanyTopCustomer.CustomerCategory,
                                CompanyTopCustomerRevenues = new List<CompanyTopCustomerRevenue>()
                            };

                            if (!updateCompanyTopCustomer.CompanyTopCustomerRevenues.IsNullOrEmpty())
                            {
                                foreach (var customerRevenue in updateCompanyTopCustomer.CompanyTopCustomerRevenues)
                                {
                                    companyTopCustomerToAdd.CompanyTopCustomerRevenues.Add(new CompanyTopCustomerRevenue() { FinancialYear = customerRevenue.FinancialYear, Revenue = customerRevenue.Revenue });
                                }
                            }

                            companyTopCustomersToInsert.Add(companyTopCustomerToAdd);
                        }
                    }
                }
            }

            if (!companyTopCustomersToInsert.IsNullOrEmpty())
            {
                await _companyTopCustomerRepository.InsertManyAsync(companyTopCustomersToInsert);
            }
            if (!companyTopCustomersToUpdate.IsNullOrEmpty())
            {
                await _companyTopCustomerRepository.UpdateManyAsync(companyTopCustomersToUpdate);
            }
            if (!companyTopCustomerRevenuesToInsert.IsNullOrEmpty())
            {
                await _companyTopCustomerRevenueRepository.InsertManyAsync(companyTopCustomerRevenuesToInsert);
            }
            if (!companyTopCustomerRevenuesToDelete.IsNullOrEmpty())
            {
                await _companyTopCustomerRevenueRepository.DeleteManyAsync(companyTopCustomerRevenuesToDelete);
            }
            if (!companyTopCustomersToDelete.IsNullOrEmpty())
            {
                await _companyTopCustomerRepository.DeleteManyAsync(companyTopCustomersToDelete);
            }
        }

        private async Task UpdateCompanyTopMarketsAsync(List<CompanyTopMarket> existingCompanyTopMarkets, List<CompanyTopMarketDto> updateCompanyTopMarkets, Guid companyInfoId)
        {
            List<CompanyTopMarket> companyTopMarketsToInsert = new List<CompanyTopMarket>();
            List<CompanyTopMarket> companyTopMarketsToDelete = new List<CompanyTopMarket>();
            List<CompanyTopMarket> companyTopMarketsToUpdate = new List<CompanyTopMarket>();
            List<CompanyTopMarketRevenue> companyTopMarketRevenuesToInsert = new List<CompanyTopMarketRevenue>();
            List<CompanyTopMarketRevenue> companyTopMarketRevenuesToDelete = new List<CompanyTopMarketRevenue>();

            // If in updateCompanyTopMarkets is null/empty means all existingCompanyTopMarket need to deleted.
            if (updateCompanyTopMarkets.IsNullOrEmpty())
            {
                companyTopMarketsToDelete = new List<CompanyTopMarket>(existingCompanyTopMarkets);
            }
            else
            {
                // if existingCompanyTopMarkets is null/empty means all updateCompanyTopMarkets need to be inserted.
                if (existingCompanyTopMarkets.IsNullOrEmpty())
                {
                    foreach (var companyTopMarket in updateCompanyTopMarkets)
                    {
                        CompanyTopMarket companyTopMarketToAdd = new CompanyTopMarket()
                        {
                            CompanyInfoId = companyInfoId,
                            RegionName = companyTopMarket.RegionName,
                            CompanyTopMarketRevenues = new List<CompanyTopMarketRevenue>()
                        };

                        if (!companyTopMarket.CompanyTopMarketRevenues.IsNullOrEmpty())
                        {
                            foreach (var marketRevenue in companyTopMarket.CompanyTopMarketRevenues)
                            {
                                companyTopMarketToAdd.CompanyTopMarketRevenues.Add(new CompanyTopMarketRevenue() { FinancialYear = marketRevenue.FinancialYear, Revenue = marketRevenue.Revenue });
                            }
                        }

                        companyTopMarketsToInsert.Add(companyTopMarketToAdd);
                    }
                }
                else
                {
                    // Check existingCompanyTopMarket whether they need to updated/deleted
                    foreach (var existingCompanyTopMarket in existingCompanyTopMarkets)
                    {
                        // Find existingCompanyTopMarket in updateCompanyTopMarkets
                        CompanyTopMarketDto updateCompanyTopMarket = updateCompanyTopMarkets.FirstOrDefault(x => x.Id == existingCompanyTopMarket.Id);

                        // If existingCompanyTopMarket found in updateCompanyTopMarkets then update existingCompanyTopMarket.
                        if (updateCompanyTopMarket != null)
                        {
                            List<CompanyTopMarketRevenue> existingCompanyTopMarketRevenues = new List<CompanyTopMarketRevenue>(existingCompanyTopMarket.CompanyTopMarketRevenues);
                            existingCompanyTopMarket.RegionName = updateCompanyTopMarket.RegionName;
                            companyTopMarketsToUpdate.Add(existingCompanyTopMarket);

                            // If companyTopMarket.CompanyTopMarketRevenues is null/empty then delete all market revenunes
                            if (updateCompanyTopMarket.CompanyTopMarketRevenues.IsNullOrEmpty())
                            {
                                if (!existingCompanyTopMarket.CompanyTopMarketRevenues.IsNullOrEmpty())
                                {
                                    companyTopMarketRevenuesToDelete.AddRange(existingCompanyTopMarket.CompanyTopMarketRevenues);
                                }
                            }
                            else
                            {
                                // If existingCompanyTopMarket don't have any revenues then add all companyTopMarket's revenues.
                                if (existingCompanyTopMarket.CompanyTopMarketRevenues.IsNullOrEmpty())
                                {
                                    foreach (var marketRevenue in updateCompanyTopMarket.CompanyTopMarketRevenues)
                                    {
                                        companyTopMarketRevenuesToInsert.Add(new CompanyTopMarketRevenue() { CompanyTopMarketId = existingCompanyTopMarket.Id, FinancialYear = marketRevenue.FinancialYear, Revenue = marketRevenue.Revenue });
                                    }
                                }
                                // If existingCompanyTopMarket has any revenues then decide whether to insert/update/delete
                                else
                                {
                                    foreach (var existingCompanyTopMarketRevenue in existingCompanyTopMarket.CompanyTopMarketRevenues)
                                    {
                                        CompanyTopMarketRevenueDto companyTopMarketRevenue = updateCompanyTopMarket.CompanyTopMarketRevenues.FirstOrDefault(x => x.FinancialYear == existingCompanyTopMarketRevenue.FinancialYear);

                                        // If existingCompanyTopMarketRevenue found in updateCompanyTopMarket.CompanyTopMarketRevenues then update existingCompanyTopMarketRevenue. 
                                        if (companyTopMarketRevenue != null)
                                        {
                                            existingCompanyTopMarketRevenue.Revenue = companyTopMarketRevenue.Revenue;
                                        }
                                        // If existingCompanyTopMarketRevenue not found in updateCompanyTopMarket.CompanyTopMarketRevenues then delete existingCompanyTopMarketRevenue. 
                                        else
                                        {
                                            companyTopMarketRevenuesToDelete.Add(existingCompanyTopMarketRevenue);
                                        }
                                    }

                                    foreach (var companyTopMarketRevenue in updateCompanyTopMarket.CompanyTopMarketRevenues)
                                    {
                                        if (!existingCompanyTopMarket.CompanyTopMarketRevenues.Any(x => x.FinancialYear == companyTopMarketRevenue.FinancialYear))
                                        {
                                            companyTopMarketRevenuesToInsert.Add(new CompanyTopMarketRevenue() { CompanyTopMarketId = existingCompanyTopMarket.Id, FinancialYear = companyTopMarketRevenue.FinancialYear, Revenue = companyTopMarketRevenue.Revenue });
                                        }
                                    }
                                }
                            }
                        }
                        // If existingCompanyTopMarket not found in updateCompanyTopMarkets then delete existingCompanyTopMarket.
                        else
                        {
                            companyTopMarketsToDelete.Add(existingCompanyTopMarket);
                        }
                    }

                    // Check updateCompanyTopMarkets whether they need to added or not
                    foreach (var updateCompanyTopMarket in updateCompanyTopMarkets)
                    {
                        // Check if existingCompanyTopMarkets has any market data of updateCompanyTopMarket and if not found inser that market
                        if (!existingCompanyTopMarkets.Any(x => x.Id == updateCompanyTopMarket.Id))
                        {
                            CompanyTopMarket companyTopMarketToAdd = new CompanyTopMarket()
                            {
                                CompanyInfoId = companyInfoId,
                                RegionName = updateCompanyTopMarket.RegionName,
                                CompanyTopMarketRevenues = new List<CompanyTopMarketRevenue>()
                            };

                            if (!updateCompanyTopMarket.CompanyTopMarketRevenues.IsNullOrEmpty())
                            {
                                foreach (var marketRevenue in updateCompanyTopMarket.CompanyTopMarketRevenues)
                                {
                                    companyTopMarketToAdd.CompanyTopMarketRevenues.Add(new CompanyTopMarketRevenue() { FinancialYear = marketRevenue.FinancialYear, Revenue = marketRevenue.Revenue });
                                }
                            }

                            companyTopMarketsToInsert.Add(companyTopMarketToAdd);
                        }
                    }
                }
            }

            if (!companyTopMarketsToInsert.IsNullOrEmpty())
            {
                await _companyTopMarketRepository.InsertManyAsync(companyTopMarketsToInsert);
            }
            if (!companyTopMarketsToUpdate.IsNullOrEmpty())
            {
                await _companyTopMarketRepository.UpdateManyAsync(companyTopMarketsToUpdate);
            }
            if (!companyTopMarketRevenuesToInsert.IsNullOrEmpty())
            {
                await _companyTopMarketRevenueRepository.InsertManyAsync(companyTopMarketRevenuesToInsert);
            }
            if (!companyTopMarketRevenuesToDelete.IsNullOrEmpty())
            {
                await _companyTopMarketRevenueRepository.DeleteManyAsync(companyTopMarketRevenuesToDelete);
            }
            if (!companyTopMarketsToDelete.IsNullOrEmpty())
            {
                await _companyTopMarketRepository.DeleteManyAsync(companyTopMarketsToDelete);
            }
        }

        private async Task UpdateCompanyTopCompetitorsAsync(List<CompanyTopCompetitor> existingCompanyTopCompetitors, List<CompanyTopCompetitorDto> updateCompanyTopCompetitors, Guid companyInfoId)
        {
            List<CompanyTopCompetitor> companyTopCompetitorsToInsert = new List<CompanyTopCompetitor>();
            List<CompanyTopCompetitor> companyTopCompetitorsToDelete = new List<CompanyTopCompetitor>();
            List<CompanyTopCompetitor> companyTopCompetitorsToUpdate = new List<CompanyTopCompetitor>();
            List<CompanyTopCompetitorRevenue> companyTopCompetitorRevenuesToInsert = new List<CompanyTopCompetitorRevenue>();
            List<CompanyTopCompetitorRevenue> companyTopCompetitorRevenuesToDelete = new List<CompanyTopCompetitorRevenue>();

            // If in updateCompanyTopCompetitors is null/empty means all existingCompanyTopCompetitor need to deleted.
            if (updateCompanyTopCompetitors.IsNullOrEmpty())
            {
                companyTopCompetitorsToDelete = new List<CompanyTopCompetitor>(existingCompanyTopCompetitors);
            }
            else
            {
                // if existingCompanyTopCompetitors is null/empty means all updateCompanyTopCompetitors need to be inserted.
                if (existingCompanyTopCompetitors.IsNullOrEmpty())
                {
                    foreach (var companyTopCompetitor in updateCompanyTopCompetitors)
                    {
                        CompanyTopCompetitor companyTopCompetitorToAdd = new CompanyTopCompetitor()
                        {
                            CompanyInfoId = companyInfoId,
                            CompetitorName = companyTopCompetitor.CompetitorName,
                            CompetitorLocation = companyTopCompetitor.CompetitorLocation,
                            CompanyTopCompetitorRevenues = new List<CompanyTopCompetitorRevenue>()
                        };

                        if (!companyTopCompetitor.CompanyTopCompetitorRevenues.IsNullOrEmpty())
                        {
                            foreach (var competitorRevenue in companyTopCompetitor.CompanyTopCompetitorRevenues)
                            {
                                companyTopCompetitorToAdd.CompanyTopCompetitorRevenues.Add(new CompanyTopCompetitorRevenue() { FinancialYear = competitorRevenue.FinancialYear, Revenue = competitorRevenue.Revenue });
                            }
                        }

                        companyTopCompetitorsToInsert.Add(companyTopCompetitorToAdd);
                    }
                }
                else
                {
                    // Check existingCompanyTopCompetitor whether they need to updated/deleted
                    foreach (var existingCompanyTopCompetitor in existingCompanyTopCompetitors)
                    {
                        // Find existingCompanyTopCompetitor in updateCompanyTopCompetitors
                        CompanyTopCompetitorDto updateCompanyTopCompetitor = updateCompanyTopCompetitors.FirstOrDefault(x => x.Id == existingCompanyTopCompetitor.Id);

                        // If existingCompanyTopCompetitor found in updateCompanyTopCompetitors then update existingCompanyTopCompetitor.
                        if (updateCompanyTopCompetitor != null)
                        {
                            List<CompanyTopCompetitorRevenue> existingCompanyTopCompetitorRevenues = new List<CompanyTopCompetitorRevenue>(existingCompanyTopCompetitor.CompanyTopCompetitorRevenues);
                            existingCompanyTopCompetitor.CompetitorName = updateCompanyTopCompetitor.CompetitorName;
                            existingCompanyTopCompetitor.CompetitorLocation = updateCompanyTopCompetitor.CompetitorLocation;
                            companyTopCompetitorsToUpdate.Add(existingCompanyTopCompetitor);

                            // If companyTopCompetitor.CompanyTopCompetitorRevenues is null/empty then delete all competitor revenunes
                            if (updateCompanyTopCompetitor.CompanyTopCompetitorRevenues.IsNullOrEmpty())
                            {
                                if (!existingCompanyTopCompetitor.CompanyTopCompetitorRevenues.IsNullOrEmpty())
                                {
                                    companyTopCompetitorRevenuesToDelete.AddRange(existingCompanyTopCompetitor.CompanyTopCompetitorRevenues);
                                }
                            }
                            else
                            {
                                // If existingCompanyTopCompetitor don't have any revenues then add all companyTopCompetitor's revenues.
                                if (existingCompanyTopCompetitor.CompanyTopCompetitorRevenues.IsNullOrEmpty())
                                {
                                    foreach (var competitorRevenue in updateCompanyTopCompetitor.CompanyTopCompetitorRevenues)
                                    {
                                        companyTopCompetitorRevenuesToInsert.Add(new CompanyTopCompetitorRevenue() { CompanyTopCompetitorId = existingCompanyTopCompetitor.Id, FinancialYear = competitorRevenue.FinancialYear, Revenue = competitorRevenue.Revenue });
                                    }
                                }
                                // If existingCompanyTopCompetitor has any revenues then decide whether to insert/update/delete
                                else
                                {
                                    foreach (var existingCompanyTopCompetitorRevenue in existingCompanyTopCompetitor.CompanyTopCompetitorRevenues)
                                    {
                                        CompanyTopCompetitorRevenueDto companyTopCompetitorRevenue = updateCompanyTopCompetitor.CompanyTopCompetitorRevenues.FirstOrDefault(x => x.FinancialYear == existingCompanyTopCompetitorRevenue.FinancialYear);

                                        // If existingCompanyTopCompetitorRevenue found in updateCompanyTopCompetitor.CompanyTopCompetitorRevenues then update existingCompanyTopCompetitorRevenue. 
                                        if (companyTopCompetitorRevenue != null)
                                        {
                                            existingCompanyTopCompetitorRevenue.Revenue = companyTopCompetitorRevenue.Revenue;
                                        }
                                        // If existingCompanyTopCompetitorRevenue not found in updateCompanyTopCompetitor.CompanyTopCompetitorRevenues then delete existingCompanyTopCompetitorRevenue. 
                                        else
                                        {
                                            companyTopCompetitorRevenuesToDelete.Add(existingCompanyTopCompetitorRevenue);
                                        }
                                    }

                                    foreach (var companyTopCompetitorRevenue in updateCompanyTopCompetitor.CompanyTopCompetitorRevenues)
                                    {
                                        if (!existingCompanyTopCompetitor.CompanyTopCompetitorRevenues.Any(x => x.FinancialYear == companyTopCompetitorRevenue.FinancialYear))
                                        {
                                            companyTopCompetitorRevenuesToInsert.Add(new CompanyTopCompetitorRevenue() { CompanyTopCompetitorId = existingCompanyTopCompetitor.Id, FinancialYear = companyTopCompetitorRevenue.FinancialYear, Revenue = companyTopCompetitorRevenue.Revenue });
                                        }
                                    }
                                }
                            }
                        }
                        // If existingCompanyTopCompetitor not found in updateCompanyTopCompetitors then delete existingCompanyTopCompetitor.
                        else
                        {
                            companyTopCompetitorsToDelete.Add(existingCompanyTopCompetitor);
                        }
                    }

                    // Check updateCompanyTopCompetitors whether they need to added or not
                    foreach (var updateCompanyTopCompetitor in updateCompanyTopCompetitors)
                    {
                        // Check if existingCompanyTopCompetitors has any competitor data of updateCompanyTopCompetitor and if not found inser that competitor
                        if (!existingCompanyTopCompetitors.Any(x => x.Id == updateCompanyTopCompetitor.Id))
                        {
                            CompanyTopCompetitor companyTopCompetitorToAdd = new CompanyTopCompetitor()
                            {
                                CompanyInfoId = companyInfoId,
                                CompetitorName = updateCompanyTopCompetitor.CompetitorName,
                                CompetitorLocation = updateCompanyTopCompetitor.CompetitorLocation,
                                CompanyTopCompetitorRevenues = new List<CompanyTopCompetitorRevenue>()
                            };

                            if (!updateCompanyTopCompetitor.CompanyTopCompetitorRevenues.IsNullOrEmpty())
                            {
                                foreach (var competitorRevenue in updateCompanyTopCompetitor.CompanyTopCompetitorRevenues)
                                {
                                    companyTopCompetitorToAdd.CompanyTopCompetitorRevenues.Add(new CompanyTopCompetitorRevenue() { FinancialYear = competitorRevenue.FinancialYear, Revenue = competitorRevenue.Revenue });
                                }
                            }

                            companyTopCompetitorsToInsert.Add(companyTopCompetitorToAdd);
                        }
                    }
                }
            }

            if (!companyTopCompetitorsToInsert.IsNullOrEmpty())
            {
                await _companyTopCompetitorRepository.InsertManyAsync(companyTopCompetitorsToInsert);
            }
            if (!companyTopCompetitorsToUpdate.IsNullOrEmpty())
            {
                await _companyTopCompetitorRepository.UpdateManyAsync(companyTopCompetitorsToUpdate);
            }
            if (!companyTopCompetitorRevenuesToInsert.IsNullOrEmpty())
            {
                await _companyTopCompetitorRevenueRepository.InsertManyAsync(companyTopCompetitorRevenuesToInsert);
            }
            if (!companyTopCompetitorRevenuesToDelete.IsNullOrEmpty())
            {
                await _companyTopCompetitorRevenueRepository.DeleteManyAsync(companyTopCompetitorRevenuesToDelete);
            }
            if (!companyTopCompetitorsToDelete.IsNullOrEmpty())
            {
                await _companyTopCompetitorRepository.DeleteManyAsync(companyTopCompetitorsToDelete);
            }
        }

        private async Task UpdateCompanyTeamMembersAsync(List<CompanyTeamMember> existingCompanyTeamMembers, List<CompanyTeamMemberDto> updateCompanyTeamMembers, Guid companyInfoId)
        {
            List<CompanyTeamMember> companyTeamMemberToInsert = new List<CompanyTeamMember>();
            List<CompanyTeamMember> companyTeamMemberToDelete = new List<CompanyTeamMember>();
            List<CompanyTeamMember> companyTeamMemberToUpdate = new List<CompanyTeamMember>();
            // existingCompanyTeamMembers is null/empty then add all updateCompanyTeamMembers
            if (existingCompanyTeamMembers.IsNullOrEmpty())
            {
                if (!updateCompanyTeamMembers.IsNullOrEmpty())
                {
                    foreach (var teamMember in updateCompanyTeamMembers)
                    {
                        companyTeamMemberToInsert.Add(new CompanyTeamMember()
                        {
                            CompanyInfoId = companyInfoId,
                            Name = teamMember.Name,
                            Designation = teamMember.Designation,
                            Department = teamMember.Department,
                            Email = teamMember.Email
                        });
                    }
                }
            }
            else
            {
                // If updateCompanyTeamMembers then delete all the existing company team members
                if (updateCompanyTeamMembers.IsNullOrEmpty())
                {
                    companyTeamMemberToDelete.AddRange(existingCompanyTeamMembers);
                }
                else
                {
                    // Check to decide whether team member needs to be deleted or updated
                    foreach (var existingCompanyTeamMember in existingCompanyTeamMembers)
                    {
                        CompanyTeamMemberDto companyTeamMember = updateCompanyTeamMembers.FirstOrDefault(x => x.Id == existingCompanyTeamMember.Id);
                        //If existingCompanyTeamMember found in updateCompanyTeamMembers so update existingCompanyTeamMember
                        if (companyTeamMember != null)
                        {
                            existingCompanyTeamMember.Name = companyTeamMember.Name;
                            existingCompanyTeamMember.Designation = companyTeamMember.Designation;
                            existingCompanyTeamMember.Department = companyTeamMember.Department;
                            existingCompanyTeamMember.Email = companyTeamMember.Email;
                            companyTeamMemberToUpdate.Add(existingCompanyTeamMember);
                        }
                        //If existingCompanyTeamMember not found in updateCompanyTeamMembers so delete existingCompanyTeamMember
                        else
                        {
                            companyTeamMemberToDelete.Add(existingCompanyTeamMember);
                        }
                    }

                    foreach (var updateCompanyTeamMember in updateCompanyTeamMembers)
                    {
                        if(!existingCompanyTeamMembers.Any(x => x.Id == updateCompanyTeamMember.Id))
                        {
                            companyTeamMemberToInsert.Add(new CompanyTeamMember()
                            {
                                CompanyInfoId = companyInfoId,
                                Name = updateCompanyTeamMember.Name,
                                Designation = updateCompanyTeamMember.Designation,
                                Department = updateCompanyTeamMember.Department,
                                Email = updateCompanyTeamMember.Email
                            });
                        }
                    }
                }
            }

            if(!companyTeamMemberToInsert.IsNullOrEmpty())
            {
                await _companyTeamMemberRepository.InsertManyAsync(companyTeamMemberToInsert);
            }
            if (!companyTeamMemberToUpdate.IsNullOrEmpty())
            {
                await _companyTeamMemberRepository.UpdateManyAsync(companyTeamMemberToUpdate);
            }
            if (!companyTeamMemberToDelete.IsNullOrEmpty())
            {
                await _companyTeamMemberRepository.DeleteManyAsync(companyTeamMemberToDelete);
            }
        }

        #endregion
    }
}
