using Volo.Abp.Modularity;

namespace EmpManagement;

[DependsOn(
    typeof(EmpManagementApplicationModule),
    typeof(EmpManagementDomainTestModule)
    )]
public class EmpManagementApplicationTestModule : AbpModule
{

}
