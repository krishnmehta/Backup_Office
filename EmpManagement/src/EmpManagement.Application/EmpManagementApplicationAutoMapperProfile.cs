using AutoMapper;

namespace EmpManagement;

public class EmpManagementApplicationAutoMapperProfile : Profile
{
    public EmpManagementApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Employee, EmployeeDto>();
        CreateMap<EmployeeDto, Employee>();
    }
}
