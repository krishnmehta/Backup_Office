using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Saturn.BusinessInsight.Dtos;
using Saturn.Dashboard.Dtos;
using Saturn.DomainModels.BusinessInsight;
using Saturn.DomainModels.Company;
using Saturn.Managers.BusinessInsight;
using Saturn.Managers.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Saturn.BusinessInsight
{
    [Authorize]
    public class BusinessInsightAppService : ApplicationService, IBusinessInsightAppService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly BusinessInsightManager _businessInsightManager;
        public BusinessInsightAppService(IRepository<Product> productRepository,
            BusinessInsightManager businessInsightManager)
        {
            _productRepository = productRepository;
            _businessInsightManager  = businessInsightManager;
        }

        /// <summary>
        /// This method is used to get all business insight products with basic details
        /// </summary>
        /// <returns>List of ProductDetailsDto object that holds details of business insight</returns>
        public async Task<List<ProductDetailsDto>> GetAllBusinessInsightProductDetailsAsync()
        {
            IQueryable<Product> productsQuery = await _productRepository.GetQueryableAsync();
            return productsQuery.Select(x => new ProductDetailsDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList();
        }

        /// <summary>
        /// This method is used to get business product
        /// </summary>
        /// <returns>List of ProductDetailsDto object that holds details of business insight</returns>
        public async Task<ProductViewDetailsDto> GetBusinessInsightProductViewDetailsByIdAsync(int id)
        {
            return ObjectMapper.Map<Product, ProductViewDetailsDto>(await _productRepository.GetAsync(x => x.Id == id));
        }

        /// <summary>
        /// This method is used to get product data point with upload data form link
        /// </summary>
        /// <param name="id">Id of the product</param>
        /// <returns>Object that hold list of product data points and upload data from link</returns>
        public async Task<ProductDataPointListDto> GetProductDataPointsWithFormLinkByIdAsync(int id)
        {
            ProductDataPointListDto productData = ObjectMapper.Map<Product, ProductDataPointListDto>(await _businessInsightManager.GetProductWithDataPointByIdAsync(id));
            return productData;
        }
    }
}
