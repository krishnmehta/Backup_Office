using Microsoft.EntityFrameworkCore;
using Saturn.DomainModels.BusinessInsight;
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
    public class ProductRepository : EfCoreRepository<SaturnDbContext, Product, int>, IProductRepository
    {
        public ProductRepository(IDbContextProvider<SaturnDbContext> dbContextProvider) : base(dbContextProvider)
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

        public async Task<Product> GetProductWithDataPointByIdAsync(int productId)
        {
            SaturnDbContext dbContext = await GetDbContextAsync();
            IQueryable<Product> productQuery = dbContext.Product.AsQueryable();

            Product product = await productQuery.Where(x => x.Id == productId)
                .Include(x => x.ProductDataUploadPoints)
                .FirstOrDefaultAsync();
            return product;
        }
    }
}
