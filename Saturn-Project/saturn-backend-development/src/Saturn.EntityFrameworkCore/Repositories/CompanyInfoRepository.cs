using Microsoft.EntityFrameworkCore;
using Saturn.DomainModels.Company;
using Saturn.EntityFrameworkCore;
using Saturn.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Saturn.Repositories
{
    public class CompanyInfoRepository : EfCoreRepository<SaturnDbContext, CompanyInfo, Guid>, ICompanyInfoRepository
    {
        public CompanyInfoRepository(IDbContextProvider<SaturnDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<CompanyInfo> GetCompanyInfoIncludeCompanyProfileDetailsByUserIdAsync(Guid userId)
        {
            SaturnDbContext dbContext = await GetDbContextAsync();
            IQueryable<CompanyInfo> companyInfoQuery = dbContext.CompanyInfo.AsQueryable();

            return await companyInfoQuery.Where(x => x.UserId == userId)
                .Include(x => x.CompanyRevenues)
                .Include(x => x.CompanyKeyProblems)
                .Include(x => x.CompanyTopProducts).ThenInclude(x => x.CompanyTopProductRevenues)
                .Include(x => x.CompanyTopCustomers).ThenInclude(x => x.CompanyTopCustomerRevenues)
                .Include(x => x.CompanyTopMarkets).ThenInclude(x => x.CompanyTopMarketRevenues)
                .Include(x => x.CompanyTopCompetitors).ThenInclude(x => x.CompanyTopCompetitorRevenues)
                .Include(x => x.CompanyTeamMembers)
                .FirstOrDefaultAsync();
        }
    }
}
