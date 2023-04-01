using Saturn.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Saturn.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class SaturnController : AbpControllerBase
{
    protected SaturnController()
    {
        LocalizationResource = typeof(SaturnResource);
    }
}
