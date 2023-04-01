using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Saturn;

[Dependency(ReplaceServices = true)]
public class SaturnBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Saturn";
}
