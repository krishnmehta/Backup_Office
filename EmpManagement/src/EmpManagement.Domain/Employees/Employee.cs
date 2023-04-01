using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace EmpManagement;

public class Employee : AuditedAggregateRoot<Guid>
{
    public string Name { get; set; }

    public int Age { get; set; }

    public string Email { get; set; }

    public float Salary { get; set; }
}
