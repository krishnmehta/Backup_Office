using Saturn.DomainModels.BusinessInsight;
using Saturn.DomainModels.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Saturn.IRepositories
{
    public interface IProductRepository : IRepository<Product, int>
    {
        Task<Product> GetProductWithDataPointByIdAsync(int productId);
    }
}
