using Saturn.DomainModels.Company;
using Saturn.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Saturn.Managers.Company
{
    public class CompanyInfoManager : DomainService
    {
        private readonly ICompanyInfoRepository _companyInfoRepository;
        public CompanyInfoManager(ICompanyInfoRepository companyInfoRepository)
        {
            _companyInfoRepository = companyInfoRepository;
        }

        public async Task<CompanyInfo> GetCompanyInfoIncludeCompanyProfileDetailsByUserIdAsync(Guid companyInfoId)
        {
            return await _companyInfoRepository.GetCompanyInfoIncludeCompanyProfileDetailsByUserIdAsync(companyInfoId);
        }
    }
}
