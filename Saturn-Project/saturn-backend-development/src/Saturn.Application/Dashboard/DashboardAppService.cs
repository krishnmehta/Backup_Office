using Microsoft.AspNetCore.Authorization;
using Microsoft.PowerBI.Api.Models;
using Microsoft.PowerBI.Api;
using Microsoft.Rest;
using Saturn.Dashboard.Dtos;
using Saturn.DomainModels.BusinessInsight;
using Saturn.PowerBiUtil.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Microsoft.Identity.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Saturn.Dashboard
{
    [Authorize]
    public class DashboardAppService : SaturnAppService, IDashboardAppService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IConfiguration _appConfiguration;
        public DashboardAppService(IRepository<Product> productRepository, IConfiguration appConfiguration)
        {
            _productRepository = productRepository;
            _appConfiguration = appConfiguration;
        }

        /// <summary>
        /// This method is used to get product dropdown details
        /// </summary>
        /// <returns>Returns list of object that holds product dropdown details</returns>
        public async Task<List<ProductDropdownDto>> GetProductDropdownDetailsAsync()
        {
            IQueryable<Product> productsQuery = await _productRepository.GetQueryableAsync();
            return productsQuery.Select(x => new ProductDropdownDto()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        /// <summary>
        /// Get embed report
        /// </summary>
        /// <param name="workspaceId">Id of workspace which contains report</param>
        /// <param name="reportId">Id of report</param>
        /// <returns>EmbedReport object containing Embed token, Embed URL, Report Id, and Report name for single embed report</returns>
        public async Task<EmbedReport> GenerateEmbedReport(Guid workspaceId, Guid reportId)
        {
            try
            {
                // For now workspaceId and reportId is being fetched from appsettings. Once we work on implementation of show reporty dynamically based
                // on business diagnosis product below code will be removed.
                workspaceId = new Guid(_appConfiguration["PowerBi:WorkspaceId"]);
                reportId = new Guid(_appConfiguration["PowerBi:ReportId"]);

                Guid additionalDatasetId = Guid.Empty;

                using (PowerBIClient pbiClient = await GetPowerBiClient())
                {
                    // Get report info
                    Report pbiReport = pbiClient.Reports.GetReportInGroup(workspaceId, reportId);

                    /*
                    Check if dataset is present for the corresponding report
                    If no dataset is present then it is a RDL report 
                    */
                    bool isRDLReport = string.IsNullOrEmpty(pbiReport.DatasetId);

                    EmbedToken embedToken;

                    if (isRDLReport)
                    {
                        // Get Embed token for RDL Report
                        embedToken = await GetEmbedTokenForRDLReport(workspaceId, reportId);
                    }
                    else
                    {
                        // Create list of dataset
                        List<Guid> datasetIds = new List<Guid>
                        {
                            // Add dataset associated to the report
                            Guid.Parse(pbiReport.DatasetId)
                        };

                        // Append additional dataset to the list to achieve dynamic binding later
                        if (additionalDatasetId != Guid.Empty)
                        {
                            datasetIds.Add(additionalDatasetId);
                        }

                        // Get Embed token
                        embedToken = await GetEmbedToken(reportId, datasetIds, workspaceId);
                    }

                    // Create new EmbedReport and return;
                    return new EmbedReport()
                    {
                        ReportId = pbiReport.Id,
                        ReportName = pbiReport.Name,
                        EmbedUrl = pbiReport.EmbedUrl,
                        EmbedToken = embedToken.Token
                    };
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
                return null;
            }
        }
        
        #region Private Methods

        /// <summary>
        /// Get Embed token for single report, multiple datasets, and an optional target workspace
        /// </summary>
        /// <returns>Embed token</returns>
        /// <remarks>This function is not supported for RDL Report</remakrs>
        private async Task<EmbedToken> GetEmbedToken(Guid reportId, IList<Guid> datasetIds, [Optional] Guid targetWorkspaceId)
        {
            using (PowerBIClient pbiClient = await GetPowerBiClient())
            {
                // Create a request for getting Embed token 
                // This method works only with new Power BI V2 workspace experience
                GenerateTokenRequestV2 tokenRequest = new GenerateTokenRequestV2(

                    reports: new List<GenerateTokenRequestV2Report>() { new GenerateTokenRequestV2Report(reportId) },

                    datasets: datasetIds.Select(datasetId => new GenerateTokenRequestV2Dataset(datasetId.ToString())).ToList(),

                    targetWorkspaces: targetWorkspaceId != Guid.Empty ? new List<GenerateTokenRequestV2TargetWorkspace>() { new GenerateTokenRequestV2TargetWorkspace(targetWorkspaceId) } : null
                );

                // Generate Embed token
                EmbedToken embedToken = pbiClient.EmbedToken.GenerateToken(tokenRequest);

                return embedToken;
            }
        }

        /// <summary>
        /// Get Embed token for RDL Report
        /// </summary>
        /// <returns>Embed token</returns>
        private async Task<EmbedToken> GetEmbedTokenForRDLReport(Guid targetWorkspaceId, Guid reportId, string accessLevel = "view")
        {
            using (PowerBIClient pbiClient = await GetPowerBiClient())
            {

                // Generate token request for RDL Report
                GenerateTokenRequest generateTokenRequestParameters = new GenerateTokenRequest(
                    accessLevel: accessLevel
                );

                // Generate Embed token
                EmbedToken embedToken = pbiClient.Reports.GenerateTokenInGroup(targetWorkspaceId, reportId, generateTokenRequestParameters);

                return embedToken;
            }
        }

        /// <summary>
        /// This method is used to get PowerBiClient
        /// </summary>
        /// <returns>Returns PowerBIClient</returns>
        private async Task<PowerBIClient> GetPowerBiClient()
        {
            TokenCredentials tokenCredentials = new TokenCredentials(await GetAccessToken(), "Bearer");
            string urlPowerBiServiceApiRoot = _appConfiguration["PowerBi:UrlPowerBiServiceApiRoot"];
            return new PowerBIClient(new Uri(urlPowerBiServiceApiRoot), tokenCredentials);
        }

        /// <summary>
        /// This method is used to Get Access token for Azure AD Application
        /// </summary>
        /// <returns>Access token</returns>
        private async Task<string> GetAccessToken()
        {
            string authorityUrl = _appConfiguration["PowerBi:AuthorityUrl"];
            string tenant = _appConfiguration["PowerBi:Tenant"];
            string applicationId = _appConfiguration["PowerBi:ApplicationId"];
            string applicationSecret = _appConfiguration["PowerBi:ApplicationSecret"];
            string[] scope = new string[] { _appConfiguration["PowerBi:Scope"] };

            // For app only authentication, we need the specific tenant id in the authority url
            string tenantSpecificURL = authorityUrl.Replace("organizations", tenant);

            IConfidentialClientApplication clientApp = ConfidentialClientApplicationBuilder
                                                                            .Create(applicationId)
                                                                            .WithClientSecret(applicationSecret)
                                                                            .WithAuthority(tenantSpecificURL)
                                                                            .Build();

            AuthenticationResult authenticationResult = await clientApp.AcquireTokenForClient(scope).ExecuteAsync();

            return authenticationResult.AccessToken;
        }
        #endregion
    }
}
