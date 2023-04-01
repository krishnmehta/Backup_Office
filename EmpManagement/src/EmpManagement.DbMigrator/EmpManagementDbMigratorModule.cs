using EmpManagement.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace EmpManagement.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(EmpManagementEntityFrameworkCoreModule),
    typeof(EmpManagementApplicationContractsModule)
    )]
public class EmpManagementDbMigratorModule : AbpModule
{

}
