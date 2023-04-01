using EmpManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EmpManagement.Permissions;

public class EmpManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(EmpManagementPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(EmpManagementPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EmpManagementResource>(name);
    }
}
