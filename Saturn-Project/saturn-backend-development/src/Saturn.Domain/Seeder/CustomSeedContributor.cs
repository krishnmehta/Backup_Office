using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Saturn.Constants;
using Saturn.DomainModels.BusinessInsight;
using Saturn.DomainModels.Company;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.Uow;

namespace Saturn.Seeder;

/* Creates initial data that is needed to property run the application
 * and make client-to-server communication possible.
 */
public class CustomSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IIdentityRoleRepository _roleRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Competency> _competencyRepository;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IRepository<NatureOfBusiness> _natureOfBusinessRepository;
    private readonly IRepository<PrimaryIndustry> _primaryIndustryRepository;
    private readonly IRepository<PrimaryEndCustomer> _primaryEndCustomerRepository;
    private readonly IRepository<KeyProblem> _keyProblemRepository;
    public CustomSeedContributor(IIdentityRoleRepository roleRepository, IGuidGenerator guidGenerator, IRepository<Product> productRepository,
        IRepository<Competency> competencyRepository,
        IRepository<NatureOfBusiness> natureOfBusinessRepository,
        IRepository<PrimaryIndustry> primaryIndustryRepository,
        IRepository<PrimaryEndCustomer> primaryEndCustomerRepository,
        IRepository<KeyProblem> keyProblemRepository)
    {
        _roleRepository = roleRepository;
        _guidGenerator = guidGenerator;
        _productRepository = productRepository;
        _competencyRepository = competencyRepository;
        _natureOfBusinessRepository = natureOfBusinessRepository;
        _primaryIndustryRepository = primaryIndustryRepository;
        _primaryEndCustomerRepository = primaryEndCustomerRepository;
        _keyProblemRepository = keyProblemRepository;
    }

    [UnitOfWork]
    public virtual async Task SeedAsync(DataSeedContext context)
    {
        await CreateRolesAsync();
        await CreateBusinessInsightProductAsync();
        await CreateCompetencyAsync();
        await CreatePrimaryAndSecondaryIndustriesAsync();
        await CreateKeyProblemsAsync();
        await CreateNatureOfBusinessesAsync();
        await CreatePrimaryEndCustomerAsync();
    }

    /// <summary>
    /// This method is used to seed role
    /// </summary>
    /// <returns>Task</returns>
    private async Task CreateRolesAsync()
    {
        List<IdentityRole> roles = await _roleRepository.GetListAsync();
        if (!roles.Any(x => x.Name == StaticRoles.Company))
        {
            IdentityRole identityRole = new IdentityRole(_guidGenerator.Create(), StaticRoles.Company);
            identityRole.IsStatic = true;
            await _roleRepository.InsertAsync(identityRole);
        }
    }

    /// <summary>
    /// This method is used to seed business insight products
    /// </summary>
    /// <returns>Task</returns>
    private async Task CreateBusinessInsightProductAsync()
    {
        if (!await _productRepository.AnyAsync())
        {
            List<Product> products = new List<Product>() {
                new Product()
                {
                    Name = "Strategic Insights",
                    Description = "According to Mercer Island Group, a top agency search firm that works with some of the world’s largest brands, a strategic insight is “a penetrating truth that elevates strategy, enabling highly differentiated tactics.",
                    TypeformLink = "N3M7JzL3",
                    QuestionnaireDescription = "This toolkit is created to help you assess your company and understand its financial status. As a direct output, you will be able to understand the level of financial staus of your company; with this information, you will be able to determine what actions are essential to ensure the growth and stability of your company.",
                    IsMainProduct = true
                },
                new Product()
                {
                    Name = "Market Insights",
                    Description = "According to Mercer Island Group, a top agency search firm that.",
                    TypeformLink = "N3M7JzL3",
                    QuestionnaireDescription = "This toolkit is created to help you assess your company and understand its financial status. As a direct output, you will be able to understand the level of financial staus of your company; with this information, you will be able to determine what actions are essential to ensure the growth and stability of your company.",
                    IsMainProduct = false
                },
                new Product()
                {
                    Name = "Financial Insights",
                    Description = "According to Mercer Island Group, a top agency search firm that.",
                    TypeformLink = "N3M7JzL3",
                    QuestionnaireDescription = "This toolkit is created to help you assess your company and understand its financial status. As a direct output, you will be able to understand the level of financial staus of your company; with this information, you will be able to determine what actions are essential to ensure the growth and stability of your company.",
                    IsMainProduct = false
                },
                new Product()
                {
                    Name = "Customer Insights",
                    Description = "According to Mercer Island Group, a top agency search firm that.",
                    TypeformLink = "N3M7JzL3",
                    QuestionnaireDescription = "This toolkit is created to help you assess your company and understand its financial status. As a direct output, you will be able to understand the level of financial staus of your company; with this information, you will be able to determine what actions are essential to ensure the growth and stability of your company.",
                    IsMainProduct = false
                },
                new Product()
                {
                    Name = "Process Insights",
                    Description = "According to Mercer Island Group, a top agency search firm that.",
                    TypeformLink = "N3M7JzL3",
                    QuestionnaireDescription = "This toolkit is created to help you assess your company and understand its financial status. As a direct output, you will be able to understand the level of financial staus of your company; with this information, you will be able to determine what actions are essential to ensure the growth and stability of your company.",
                    IsMainProduct = false
                },
                new Product()
                {
                    Name = "People Insights",
                    Description = "According to Mercer Island Group, a top agency search firm that.",
                    TypeformLink = "N3M7JzL3",
                    QuestionnaireDescription = "This toolkit is created to help you assess your company and understand its financial status. As a direct output, you will be able to understand the level of financial staus of your company; with this information, you will be able to determine what actions are essential to ensure the growth and stability of your company.",
                    IsMainProduct = false
                }
            };

            await _productRepository.InsertManyAsync(products);
        }
    }

    /// <summary>
    /// This method is used to seed competency
    /// </summary>
    /// <returns>Task</returns>
    private async Task CreateCompetencyAsync()
    {
        if (!await _competencyRepository.AnyAsync())
        {
            List<Competency> competencies = new List<Competency>
            {
                new Competency() { Title = "MANAGING YOURSELF"},
                new Competency() { Title = "DECISION MAKING"},
                new Competency() { Title = "INNOVATION"},
                new Competency() { Title = "SUSTAINABILITY"},
                new Competency() { Title = "Change Management"},
                new Competency() { Title = "Motivation"},
                new Competency() { Title = "STRATEGY FORMULATION"},
                new Competency() { Title = "BUSINESS PLANNING"},
                new Competency() { Title = "GROWTH STRATEGY"},
                new Competency() { Title = "MARKET ENTRY STRATEGY"},
                new Competency() { Title = "GTM STRATEGY"},
                new Competency() { Title = "MARKET ANALYSIS"},
                new Competency() { Title = "BI & ANALYTICS"},
                new Competency() { Title = "ETHICS"},
                new Competency() { Title = "POLICIES"},
                new Competency() { Title = "PRODUCTIVITY"},
                new Competency() { Title = "OPERATIONS EXCELLENCE"},
                new Competency() { Title = "LOGISTICS"},
                new Competency() { Title = "COST MANAGEMENT"},
                new Competency() { Title = "DATA ANALYTICS"},
                new Competency() { Title = "ARTIFICIAL INTELLIGENCE"},
                new Competency() { Title = "DIGITAL TRANSFORMATION"},
                new Competency() { Title = "SAS"},
                new Competency() { Title = "BRANDING"},
                new Competency() { Title = "MEDIA"},
                new Competency() { Title = "TRADITIONAL MARKETING"},
                new Competency() { Title = "DIGITAL MARKETING"},
                new Competency() { Title = "INTERNATIONAL TRADE"},
                new Competency() { Title = "CUSTOMER MANAGEMENT"},
                new Competency() { Title = "KEY ACCOUNT MANAGEMENT"},
                new Competency() { Title = "DISTRIBUTION"},
                new Competency() { Title = "TRADITIONAL TRADE"},
                new Competency() { Title = "MODERN TRADE"},
                new Competency() { Title = "DIGITAL TRADE"},
                new Competency() { Title = "B2B SALES"},
                new Competency() { Title = "B2C SALES"},
                new Competency() { Title = "INVESTMENTS"},
                new Competency() { Title = "FINANCIAL MANAGEMENT"},
                new Competency() { Title = "VENTURE CAPITAL"},
                new Competency() { Title = "RISK MANAGEMENT"},
                new Competency() { Title = "COST CONTROL"}
            };

            await _competencyRepository.InsertManyAsync(competencies);
        }
    }

    /// <summary>
    /// This method is used to seed primary and secondry industries
    /// </summary>
    /// <returns>Task</returns>
    private async Task CreatePrimaryAndSecondaryIndustriesAsync()
    {
        if (!await _primaryIndustryRepository.AnyAsync())
        {
            List<PrimaryIndustry> primaryIndustries = new List<PrimaryIndustry>()
            {
                new PrimaryIndustry()
                {
                    PrimaryIndustryName = "Agriculture, Forestry, Fishing and Hunting",
                    SecondaryIndustries = new List<SecondaryIndustry>()
                    {
                        new SecondaryIndustry() { SecondaryIndustryName = "Crop Production"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Animal Production and AquacultureT"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Forestry and LoggingT"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Fishing, Hunting and TrappingT"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Support Activities for Agriculture and ForestryT"},
                    }
                },
                new PrimaryIndustry()
                {
                    PrimaryIndustryName = "Mining, Quarrying, and Oil and Gas Extraction",
                    SecondaryIndustries = new List<SecondaryIndustry> ()
                    {
                        new SecondaryIndustry() { SecondaryIndustryName = "Oil and Gas Extraction"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Mining(except Oil and Gas)"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Support Activities for Mining"}
                    }
                },
                new PrimaryIndustry()
                {
                    PrimaryIndustryName = "Construction",
                    SecondaryIndustries = new List<SecondaryIndustry> ()
                    {
                        new SecondaryIndustry() { SecondaryIndustryName = "Construction of Buildings"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Heavy and Civil Engineering Construction"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Specialty Trade Contractors"}
                    }
                },
                new PrimaryIndustry()
                {
                    PrimaryIndustryName = "Manufacturing",
                    SecondaryIndustries = new List<SecondaryIndustry> ()
                    {
                        new SecondaryIndustry() { SecondaryIndustryName = "Food Manufacturing"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Beverage and Tobacco Product Manufacturing"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Textile Mills"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Textile Product Mills"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Apparel Manufacturing"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Leather and Allied Product Manufacturing"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Wood Product Manufacturing"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Paper Manufacturing"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Printing and Related Support Activities"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Petroleum and Coal Products Manufacturing"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Chemical Manufacturing"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Plastics and Rubber Products Manufacturing"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Nonmetallic Mineral Product Manufacturing"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Primary Metal Manufacturing"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Fabricated Metal Product Manufacturing"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Machinery Manufacturing"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Computer and Electronic Product Manufacturing"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Electrical Equipment, Appliance, and Component Manufacturing"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Transportation Equipment Manufacturing"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Furniture and Related Product Manufacturing"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Medical Equipments Manufacturing"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Miscellaneous Manufacturing"}
                    }
                },
                new PrimaryIndustry()
                {
                    PrimaryIndustryName = "Wholesale Trade",
                    SecondaryIndustries = new List<SecondaryIndustry> ()
                    {
                        new SecondaryIndustry() { SecondaryIndustryName = "Merchant Wholesalers, Durable Goods"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Merchant Wholesalers, Nondurable Goods"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Wholesale Electronic Markets and Agents and Brokers"}
                    }
                },
                new PrimaryIndustry()
                {
                    PrimaryIndustryName = "Retail Trade",
                    SecondaryIndustries = new List<SecondaryIndustry> ()
                    {
                        new SecondaryIndustry() { SecondaryIndustryName = "Motor Vehicle and Parts Dealers"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Furniture and Home Furnishings Stores"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Electronics and Appliance Stores"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Building Material and Garden Equipment and Supplies Dealers"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Food and Beverage Stores"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Health and Personal Care Stores"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Gasoline Stations"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Clothing and Clothing Accessories Stores"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Sporting Goods, Hobby, Musical Instrument, and Book Stores"},
                        new SecondaryIndustry() { SecondaryIndustryName = "General Merchandise Stores"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Miscellaneous Store Retailers"},
                        new SecondaryIndustry() { SecondaryIndustryName = "Nonstore Retailers"}
                    }
                },
                new PrimaryIndustry()
                {
                    PrimaryIndustryName = "Transportation and Warehousing",
                    SecondaryIndustries = new List<SecondaryIndustry> ()
                    {
                        new SecondaryIndustry() { SecondaryIndustryName ="Air Transportation"},
                        new SecondaryIndustry() { SecondaryIndustryName ="Rail Transportation"},
                        new SecondaryIndustry() { SecondaryIndustryName ="Water Transportation"},
                        new SecondaryIndustry() { SecondaryIndustryName ="Truck Transportation"},
                        new SecondaryIndustry() { SecondaryIndustryName ="Transit and Ground Passenger Transportation"},
                        new SecondaryIndustry() { SecondaryIndustryName ="Pipeline Transportation"},
                        new SecondaryIndustry() { SecondaryIndustryName ="Scenic and Sightseeing Transportation"},
                        new SecondaryIndustry() { SecondaryIndustryName ="Support Activities for Transportation"},
                        new SecondaryIndustry() { SecondaryIndustryName ="Postal Service"},
                        new SecondaryIndustry() { SecondaryIndustryName ="Couriers and Messengers"},
                        new SecondaryIndustry() { SecondaryIndustryName ="Warehousing and Storage"}
                    }
                },
                new PrimaryIndustry()
                {
                    PrimaryIndustryName = "Information",
                    SecondaryIndustries = new List<SecondaryIndustry> ()
                    {
                        new SecondaryIndustry() { SecondaryIndustryName ="Publishing Industries (except Internet)"},
                        new SecondaryIndustry() { SecondaryIndustryName ="Motion Picture and Sound Recording Industries"},
                        new SecondaryIndustry() { SecondaryIndustryName ="Broadcasting (except Internet)"},
                        new SecondaryIndustry() { SecondaryIndustryName ="Telecommunications"},
                        new SecondaryIndustry() { SecondaryIndustryName ="Data Processing, Hosting, and Related Services"},
                        new SecondaryIndustry() { SecondaryIndustryName ="Other Information Services"}
                    }
                },
                new PrimaryIndustry()
                {
                    PrimaryIndustryName = "Finance and Insurance",
                    SecondaryIndustries = new List<SecondaryIndustry> ()
                    {
                        new SecondaryIndustry() { SecondaryIndustryName ="Monetary Authorities-Central Bank"},
                        new SecondaryIndustry() { SecondaryIndustryName ="Credit Intermediation and Related Activities"},
                        new SecondaryIndustry() { SecondaryIndustryName ="Securities, Commodity Contracts, and Other Financial Investments and Related Activities"},
                        new SecondaryIndustry() { SecondaryIndustryName ="Insurance Carriers and Related Activities"},
                        new SecondaryIndustry() { SecondaryIndustryName ="Funds, Trusts, and Other Financial Vehicles"},
                    }
                },
                new PrimaryIndustry()
                {
                    PrimaryIndustryName = "Real Estate and Rental and Leasing",
                    SecondaryIndustries = new List<SecondaryIndustry> ()
                    {
                        new SecondaryIndustry() { SecondaryIndustryName ="Real Estate"},
                        new SecondaryIndustry() { SecondaryIndustryName ="Rental and Leasing Services"},
                        new SecondaryIndustry() { SecondaryIndustryName ="Lessors of Nonfinancial Intangible Assets (except Copyrighted Works)"},
                    }
                },
                new PrimaryIndustry()
                {
                    PrimaryIndustryName = "Professional, Scientific, and Technical Services",
                    SecondaryIndustries = new List<SecondaryIndustry> ()
                    {
                        new SecondaryIndustry() { SecondaryIndustryName ="Professional, Scientific, and Technical Services"}
                    }
                },
                new PrimaryIndustry()
                {
                    PrimaryIndustryName = "Management of Companies and Enterprises",
                    SecondaryIndustries = new List<SecondaryIndustry> ()
                    {
                        new SecondaryIndustry() { SecondaryIndustryName ="Management of Companies and Enterprises"}
                    }
                },
                new PrimaryIndustry()
                {
                    PrimaryIndustryName = "Administrative and Support and Waste Management and Remediation Services",
                    SecondaryIndustries = new List<SecondaryIndustry> ()
                    {
                        new SecondaryIndustry() { SecondaryIndustryName ="Administrative and Support Services"},
                        new SecondaryIndustry() { SecondaryIndustryName ="Waste Management and Remediation Services"},
                    }
                },
                new PrimaryIndustry()
                {
                    PrimaryIndustryName = "Educational Services",
                    SecondaryIndustries = new List<SecondaryIndustry> ()
                    {
                        new SecondaryIndustry() { SecondaryIndustryName = "Educational Services"}
                    }
                },
                new PrimaryIndustry()
                {
                    PrimaryIndustryName = "Health Care and Social Assistance",
                    SecondaryIndustries = new List<SecondaryIndustry> ()
                    {
                        new SecondaryIndustry() { SecondaryIndustryName = " Ambulatory Health Care ServicesT"},
                        new SecondaryIndustry() { SecondaryIndustryName = " HospitalsT"},
                        new SecondaryIndustry() { SecondaryIndustryName = " Nursing and Residential Care FacilitiesT"},
                        new SecondaryIndustry() { SecondaryIndustryName = " Social AssistanceT"}
                    }
                },
                new PrimaryIndustry()
                {
                    PrimaryIndustryName = "Arts, Entertainment, and Recreation",
                    SecondaryIndustries = new List<SecondaryIndustry> ()
                    {
                        new SecondaryIndustry() { SecondaryIndustryName = " Performing Arts, Spectator Sports, and Related Industries"},
                        new SecondaryIndustry() { SecondaryIndustryName = " Museums, Historical Sites, and Similar Institutions"},
                        new SecondaryIndustry() { SecondaryIndustryName = " Amusement, Gambling, and Recreation Industries"}
                    }
                },
                new PrimaryIndustry()
                {
                    PrimaryIndustryName = "Accommodation and Food Services",
                    SecondaryIndustries = new List<SecondaryIndustry> ()
                    {
                        new SecondaryIndustry() { SecondaryIndustryName = " Accommodation"},
                        new SecondaryIndustry() { SecondaryIndustryName = " Food Services and Drinking Places"}
                    }
                },
                new PrimaryIndustry()
                {
                    PrimaryIndustryName = "Other Services (except Public Administration)",
                    SecondaryIndustries = new List<SecondaryIndustry> ()
                    {
                        new SecondaryIndustry() { SecondaryIndustryName = " Repair and Maintenance"},
                        new SecondaryIndustry() { SecondaryIndustryName = " Personal and Laundry Services"},
                        new SecondaryIndustry() { SecondaryIndustryName = " Religious, Grantmaking, Civic, Professional, and Similar Organizations"},
                        new SecondaryIndustry() { SecondaryIndustryName = " Private Households"}
                    }
                }
            };

            await _primaryIndustryRepository.InsertManyAsync(primaryIndustries);
        }
    }

    /// <summary>
    /// This method is used to seed primary end customers
    /// </summary>
    /// <returns>Task</returns>
    private async Task CreatePrimaryEndCustomerAsync()
    {
        if (!await _primaryEndCustomerRepository.AnyAsync())
        {
            List<PrimaryEndCustomer> primaryEndCustomers = new List<PrimaryEndCustomer>()
            {
                new PrimaryEndCustomer() { Customer = "Business" },
                new PrimaryEndCustomer() { Customer = "Government" },
                new PrimaryEndCustomer() { Customer = "Individual" }
            };
            await _primaryEndCustomerRepository.InsertManyAsync(primaryEndCustomers);
        }
    }

    /// <summary>
    /// This method is used to seed nature of businesses
    /// </summary>
    /// <returns>Task</returns>
    private async Task CreateNatureOfBusinessesAsync()
    {
        if (!await _natureOfBusinessRepository.AnyAsync())
        {
            List<NatureOfBusiness> natureOfBusiness = new List<NatureOfBusiness>()
            {
                new NatureOfBusiness() { BusinessActivity = "Manufacturing" },
                new NatureOfBusiness() { BusinessActivity = "Service" },
                new NatureOfBusiness() { BusinessActivity = "Trading" }
            };
            await _natureOfBusinessRepository.InsertManyAsync(natureOfBusiness);
        }
    }

    /// <summary>
    /// This method is used to seed key problems
    /// </summary>
    /// <returns>Task</returns>
    private async Task CreateKeyProblemsAsync()
    {
        if (!await _keyProblemRepository.AnyAsync())
        {
            List<KeyProblem> keyProblems = new List<KeyProblem>()
            {
                new KeyProblem() { Problem = "Difficulty in targeting and acquiring new customers" },
                new KeyProblem() { Problem = "Lack of structured business plan" },
                new KeyProblem() { Problem = "Lack of understanding of direct and indirect competitors" },
                new KeyProblem() { Problem = "Difficulty in identifying, managing and qualifying leads" },
                new KeyProblem() { Problem = "Facing difficulty in building brand presence & visibility online" },
                new KeyProblem() { Problem = "Difficulty in identifying the optimal go to market model" },
                new KeyProblem() { Problem = "Difficulty in identifying new geographies for expansion" },
                new KeyProblem() { Problem = "Difficulty in achieving breakout revenue growth" },
                new KeyProblem() { Problem = "Lack of visibility product's performance in the market place" },
                new KeyProblem() { Problem = "Difficulty in leveraging key customers for growth" },
                new KeyProblem() { Problem = "Difficulty in market share improvement through competitive positioning" },
                new KeyProblem() { Problem = "Difficulty in raising/ accessing capital to fund growth" },
                new KeyProblem() { Problem = "Difficulty in identifying growth inhibiting factors" },
                new KeyProblem() { Problem = "Leadership and Strategic Management" },
                new KeyProblem() { Problem = "Lack of use of technology to achieve breakout growth" }
            };
            await _keyProblemRepository.InsertManyAsync(keyProblems);
        }
    }
}
