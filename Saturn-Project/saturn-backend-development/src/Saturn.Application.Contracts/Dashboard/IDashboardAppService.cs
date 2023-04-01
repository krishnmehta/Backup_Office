using Saturn.Dashboard.Dtos;
using Saturn.PowerBiUtil.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Saturn.Dashboard
{
    public interface IDashboardAppService : IApplicationService
    {
        /// <summary>
        /// This method is used to get product dropdown details
        /// </summary>
        /// <returns>Returns list of object that holds product dropdown details</returns>
        Task<List<ProductDropdownDto>> GetProductDropdownDetailsAsync();

        /// <summary>
        /// Get embed report
        /// </summary>
        /// <param name="workspaceId">Id of workspace which contains report</param>
        /// <param name="reportId">Id of report</param>
        /// <returns>EmbedReport object containing Embed token, Embed URL, Report Id, and Report name for single embed report</returns>
        Task<EmbedReport> GenerateEmbedReport(Guid workspaceId, Guid reportId);
    }
}
