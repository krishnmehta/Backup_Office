using Saturn.DomainModels.BusinessInsight;
using Saturn.DomainModels.Company;
using Saturn.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Saturn.Managers.BusinessInsight
{
    public class BusinessInsightManager : DomainService
    {
        private readonly IProductRepository _productRepository;
        public BusinessInsightManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetProductWithDataPointByIdAsync(int productId)
        {
            return await _productRepository.GetProductWithDataPointByIdAsync(productId);
        }
    }
}
