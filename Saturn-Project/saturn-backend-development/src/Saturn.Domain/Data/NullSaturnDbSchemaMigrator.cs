using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Saturn.Data;

/* This is used if database provider does't define
 * ISaturnDbSchemaMigrator implementation.
 */
public class NullSaturnDbSchemaMigrator : ISaturnDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
