using Volo.Abp.Settings;
public class EmailSettingProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(
            new SettingDefinition("Smtp.Host"),
            new SettingDefinition("Smtp.Port"),
            new SettingDefinition("Smtp.UserName"),
             new SettingDefinition("Smtp.Password", isEncrypted: true),
            new SettingDefinition("Smtp.EnableSsl"),
            new SettingDefinition("Smtp.UseDefaultCredentials", "false"),
            new SettingDefinition("DefaultFromAddress", "false"),
            new SettingDefinition("DefaultFromDisplayName", "false")
        );
    }
}