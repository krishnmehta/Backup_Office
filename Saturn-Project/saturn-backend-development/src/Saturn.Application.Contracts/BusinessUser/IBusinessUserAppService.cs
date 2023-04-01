using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Saturn.BusinessUser.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;

namespace Saturn.BusinessUser
{
    public interface IBusinessUserAppService : IApplicationService
    {
        /// <summary>
        /// This method is used to get details for NDA sign page
        /// </summary>
        /// <returns>NdaDetailsDto object that holds details for NDA</returns>
        Task<NdaDetailsDto> GetNdaDetailsAsync();

        /// <summary>
        /// This method is used to sign the NDA
        /// </summary>
        /// <returns>Task</returns>
        Task SignNdaAsync();

        /// <summary>
        /// This method is used to get the status of user onboarding
        /// </summary>
        /// <returns>OnboardingDto object that holds status of user onboarding</returns>
        Task<OnboardingDto> GetOnboardingStatus();

        /// <summary>
        /// This method is used to get all competencies
        /// </summary>
        /// <returns>List of competencydto object</returns>
        Task<List<CompetencyDto>> GetAllCompetencyAsync();

        /// <summary>
        /// This method is used to get personal info.
        /// </summary>
        /// <returns>Returns PersonalInfoDto that holds personal info</returns>
        Task<PersonalInfoDto> GetPersonalInfoAsync();

        /// <summary>
        /// This method is used to upload professional photo
        /// </summary>
        /// <param name="personalInfoForm">Object that holds personal info details</param>
        /// <returns>Return UploadImageResponse object that contains the key of uploaded image</returns>
        Task<UploadImageResponse> UploadProfessionalPhoto([FromForm] UpdateProfessionalPhotoDto personalInfoForm);

        /// <summary>
        /// This method is used to save personal info.
        /// </summary>
        /// <returns>Task</returns>
        Task UpdatePersonalInfoAsync(UpdatePersonalInfoDto personalInfo);

        /// <summary>
        /// This method is used to get company info
        /// </summary>
        /// <returns>Returns the object that holds company info details</returns>
        Task<CompanyInfoDto> GetCompanyInfoAsync();

        /// <summary>
        /// This method is used to upload company logo
        /// </summary>
        /// <param name="input">Object that holds logo</param>
        /// <returns>Return UploadImageResponse object that contains the key of uploaded image</returns>
        Task<UploadImageResponse> UploadCompanyLogo(UploadCompanyLogoDto input);

        /// <summary>
        /// This method is used to update company info
        /// </summary>
        /// <param name="updateCompanyInfoObj">Object that holds company info details to update</param>
        /// <param name="companyLogoName">Name of company logo</param>
        /// <returns>Task</returns>
        Task UpdateCompanyInfoAsync(UpdateCompanyInfoDto updateCompanyInfoObj);

        /// <summary>
        /// This method is used to get all nature of businesses.
        /// </summary>
        /// <returns>List of nature of businesses</returns>
        Task<List<NatureOfBusinessDto>> GetAllNatureOfBusinessesAsync();

        /// <summary>
        /// This method is used to get all primary industries.
        /// </summary>
        /// <returns>List of primary industries</returns>
        Task<List<PrimaryIndustryDto>> GetAllPrimaryIndustriesAsync();

        /// <summary>
        /// This method is used to get all secondary industries.
        /// </summary>
        /// <returns>List of secondary industries</returns>
        Task<List<SecondaryIndustryDto>> GetAllSecondaryIndustriesAsync();

        /// <summary>
        /// This method is used to get all primary end customers.
        /// </summary>
        /// <returns>List of primary end customers</returns>
        Task<List<PrimaryEndCustomerDto>> GetAllPrimaryEndCustomersAsync();

        /// <summary>
        /// This method is used to get all key problems.
        /// </summary>
        /// <returns>List of key problems</returns>
        Task<List<KeyProblemDto>> GetAllKeyProblemsAsync();

        /// <summary>
        /// This method is used to get company info form link
        /// </summary>
        /// <returns>Form link object that contains link of form</returns>
        Formlink GetCompanyInfoFormLinkAsync();

        /// <summary>
        /// This method is used to get personal info form link
        /// </summary>
        /// <returns>Form link object that contains link of form</returns>
        Formlink GetPersonalInfoFormLinkAsync();

        /// <summary>
        /// This method is used to set personal info status as submitted
        /// </summary>
        /// <returns>Task</returns>
        Task SetPersonalInfoStatusSubmittedAsync();

        /// <summary>
        /// This method is used to set company info status as submitted
        /// </summary>
        /// <returns>Task</returns>
        Task SetCompanyInfoStatusSubmittedAsync();
    }
}
