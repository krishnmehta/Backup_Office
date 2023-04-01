using System.Threading.Tasks;

namespace EmpManagement.Data;

public interface IEmpManagementDbSchemaMigrator
{
    Task MigrateAsync();
}
