using System;
using System.Collections.Generic;
using System.Text;
using Saturn.Localization;
using Volo.Abp.Application.Services;

namespace Saturn;

/* Inherit your application services from this class.
 */
public abstract class SaturnAppService : ApplicationService
{
    protected SaturnAppService()
    {
        LocalizationResource = typeof(SaturnResource);
    }
}
