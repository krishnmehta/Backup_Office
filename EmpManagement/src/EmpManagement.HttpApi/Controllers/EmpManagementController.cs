using EmpManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EmpManagement.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class EmpManagementController : AbpControllerBase
{
    protected EmpManagementController()
    {
        LocalizationResource = typeof(EmpManagementResource);
    }
}
