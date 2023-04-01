using Microsoft.EntityFrameworkCore;
using Saturn.DomainModels.Company;
using Saturn.EntityFrameworkCore;
using Saturn.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Saturn.Repositories
{
    public class CompanyTopProductRepository : EfCoreRepository<SaturnDbContext, CompanyTopProduct, int>, ICompanyTopProductRepository
    {
        public CompanyTopProductRepository(IDbContextProvider<SaturnDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<CompanyTopProduct>> GetCompanyTopProductsWithRevenueByCompanyIdAsync(Guid companyInfoId)
        {
            var dbContext = await GetDbContextAsync();
            var query = dbContext.CompanyTopProduct.AsQueryable();
            var list = await query.Where(x => x.CompanyInfoId == companyInfoId).Include(x => x.CompanyTopProductRevenues).ToListAsync();
            return list;
        }
    }
}
