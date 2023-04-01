using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EmpManagement;

public interface IEmployeeAppService :
    ICrudAppService< //Defines CRUD methods
        EmployeeDto, //Used to show employees
        Guid, //Primary key of the employees entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateEmployeeDto> //Used to create/update a employee
{

}
