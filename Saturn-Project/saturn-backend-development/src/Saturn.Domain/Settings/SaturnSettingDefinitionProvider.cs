using Volo.Abp.Settings;

namespace Saturn.Settings;

public class SaturnSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(SaturnSettings.MySetting1));
    }
}
