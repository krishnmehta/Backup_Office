using Volo.Abp.Modularity;

namespace Saturn;

[DependsOn(
    typeof(SaturnApplicationModule),
    typeof(SaturnDomainTestModule)
    )]
public class SaturnApplicationTestModule : AbpModule
{

}
