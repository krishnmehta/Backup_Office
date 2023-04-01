using Saturn.BusinessInsight.Profiler;
using Saturn.BusinessUser.Profiler;
using System;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.Timing;

namespace Saturn;

[DependsOn(
    typeof(SaturnDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(SaturnApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class SaturnApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<SaturnApplicationModule>();
            options.AddMaps<AccountAutoMapperProfile>();
            options.AddMaps<BusinessInsightAutoMapperProfile>();
            options.AddMaps<BusinessUserAutoMapperProfile>();
        });

        Configure<AbpClockOptions>(options =>
        {
            options.Kind = DateTimeKind.Utc;
        });
    }
}
