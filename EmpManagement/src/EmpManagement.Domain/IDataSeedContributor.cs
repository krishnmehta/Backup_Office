using System;
using System.Threading.Tasks;
using EmpManagement;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace EmpManagement;

public class EmpManagementDataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Employee, Guid> _employeeRepository;

    public EmpManagementDataSeederContributor(IRepository<Employee, Guid> employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _employeeRepository.GetCountAsync() <= 0)
        {
            await _employeeRepository.InsertAsync(
                new Employee
                {
                    Name = "Krishn",
                    Age = 21,
                    Email = "kri@gmail.com",
                    Salary = 1900.84f
                },
                autoSave: true
            );

            await _employeeRepository.InsertAsync(
                new Employee
                {
                    Name = "ramesh",
                    Age = 23,
                    Email = "rame@gmail.com",
                    Salary = 1700.20f
                },
                autoSave: true
            );
        }
    }
}
