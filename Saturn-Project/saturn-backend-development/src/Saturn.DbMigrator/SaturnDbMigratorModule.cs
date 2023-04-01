using Saturn.EntityFrameworkCore;
using System;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;
using Volo.Abp.Timing;

namespace Saturn.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(SaturnEntityFrameworkCoreModule),
    typeof(SaturnApplicationContractsModule)
    )]
public class SaturnDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);

        Configure<AbpClockOptions>(options =>
        {
            options.Kind = DateTimeKind.Utc;
        });
    }
}
