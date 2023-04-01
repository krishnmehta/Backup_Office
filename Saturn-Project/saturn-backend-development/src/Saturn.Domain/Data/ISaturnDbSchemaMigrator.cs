using System.Threading.Tasks;

namespace Saturn.Data;

public interface ISaturnDbSchemaMigrator
{
    Task MigrateAsync();
}
