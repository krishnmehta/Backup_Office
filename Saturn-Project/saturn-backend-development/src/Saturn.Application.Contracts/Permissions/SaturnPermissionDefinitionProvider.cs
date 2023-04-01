using Saturn.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Saturn.Permissions;

public class SaturnPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(SaturnPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(SaturnPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SaturnResource>(name);
    }
}
