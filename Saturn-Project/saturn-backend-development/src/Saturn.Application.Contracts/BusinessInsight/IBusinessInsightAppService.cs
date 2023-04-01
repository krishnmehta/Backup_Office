using Saturn.BusinessInsight.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Saturn.BusinessInsight
{
    public interface IBusinessInsightAppService : IApplicationService
    {
        /// <summary>
        /// This method is used to get all business insight products with basic details
        /// </summary>
        /// <returns>List of ProductDetailsDto object that holds details of business insight</returns>
        Task<List<ProductDetailsDto>> GetAllBusinessInsightProductDetailsAsync();

        /// <summary>
        /// This method is used to get business product
        /// </summary>
        /// <returns>List of ProductDetailsDto object that holds details of business insight</returns>
        Task<ProductViewDetailsDto> GetBusinessInsightProductViewDetailsByIdAsync(int id);

        /// <summary>
        /// This method is used to get product data point with upload data form link
        /// </summary>
        /// <param name="id">Id of the product</param>
        /// <returns>Object that hold list of product data points and upload data from link</returns>
        Task<ProductDataPointListDto> GetProductDataPointsWithFormLinkByIdAsync(int id);
    }
}
