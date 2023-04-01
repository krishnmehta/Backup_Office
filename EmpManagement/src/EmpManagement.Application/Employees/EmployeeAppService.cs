using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace EmpManagement;

public class EmployeeAppService :
    CrudAppService<
        Employee, //The Employee entity
        EmployeeDto, //Used to show Employee
        Guid, //Primary key of the Employee entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateEmployeeDto>, //Used to create/update a Employee
    IEmployeeAppService //implement the IEmployeeAppService
{
    public EmployeeAppService(IRepository<Employee, Guid> repository)
        : base(repository)
    {

    }
}
