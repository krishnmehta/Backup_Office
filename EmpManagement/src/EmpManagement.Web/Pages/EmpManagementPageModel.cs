using EmpManagement.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EmpManagement.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class EmpManagementPageModel : AbpPageModel
{
    protected EmpManagementPageModel()
    {
        LocalizationResourceType = typeof(EmpManagementResource);
    }
}
