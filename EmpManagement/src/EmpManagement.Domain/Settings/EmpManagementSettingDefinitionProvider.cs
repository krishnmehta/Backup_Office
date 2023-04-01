using Volo.Abp.Settings;

namespace EmpManagement.Settings;

public class EmpManagementSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(EmpManagementSettings.MySetting1));
    }
}
