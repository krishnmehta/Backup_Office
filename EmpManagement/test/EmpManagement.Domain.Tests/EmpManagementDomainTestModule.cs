using EmpManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EmpManagement;

[DependsOn(
    typeof(EmpManagementEntityFrameworkCoreTestModule)
    )]
public class EmpManagementDomainTestModule : AbpModule
{

}
