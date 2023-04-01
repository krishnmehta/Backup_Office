using System;
using System.Collections.Generic;
using System.Text;
using EmpManagement.Localization;
using Volo.Abp.Application.Services;

namespace EmpManagement;

/* Inherit your application services from this class.
 */
public abstract class EmpManagementAppService : ApplicationService
{
    protected EmpManagementAppService()
    {
        LocalizationResource = typeof(EmpManagementResource);
    }
}
