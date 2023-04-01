using Saturn.DomainModels.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Saturn.Managers
{
    public class CustomerTopProductManager : DomainService
    {
        private readonly ICompanyTopProductRepository _companyTopProductManager;
        public CustomerTopProductManager(ICompanyTopProductRepository companyTopProductManager)
        {
            _companyTopProductManager= companyTopProductManager;
        }

        public async Task<List<CompanyTopProduct>> GetCompanyTopProductsWithRevenueByCompanyIdAsync(Guid companyInfoId)
        {
            return await _companyTopProductManager.GetCompanyTopProductsWithRevenueByCompanyIdAsync(companyInfoId);
        }
    }
}
