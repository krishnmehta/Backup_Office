using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EmpManagement.Data;

/* This is used if database provider does't define
 * IEmpManagementDbSchemaMigrator implementation.
 */
public class NullEmpManagementDbSchemaMigrator : IEmpManagementDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
