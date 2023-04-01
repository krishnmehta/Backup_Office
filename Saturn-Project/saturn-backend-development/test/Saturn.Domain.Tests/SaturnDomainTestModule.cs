using Saturn.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Saturn;

[DependsOn(
    typeof(SaturnEntityFrameworkCoreTestModule)
    )]
public class SaturnDomainTestModule : AbpModule
{

}
