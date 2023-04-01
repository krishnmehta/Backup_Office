using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace EmpManagement.Web;

[Dependency(ReplaceServices = true)]
public class EmpManagementBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "EmpManagement";
}
