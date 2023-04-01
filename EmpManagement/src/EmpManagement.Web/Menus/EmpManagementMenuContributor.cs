using System.Threading.Tasks;
using EmpManagement.Localization;
using EmpManagement.MultiTenancy;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace EmpManagement.Web.Menus;

public class EmpManagementMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<EmpManagementResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                EmpManagementMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );
            context.Menu.AddItem(
        new ApplicationMenuItem(
            "EmpManagement",
            l["Menu:EmpManagement"],
            icon: "fa fa-book"
        ).AddItem(
            new ApplicationMenuItem(
                "Empmanagement.Employees",
                l["Menu:Employees"],
                url: "/Employees"
            )
        )
    );


        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        return Task.CompletedTask;
    }
}
