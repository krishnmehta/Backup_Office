using System;
using System.ComponentModel.DataAnnotations;

namespace EmpManagement;

public class CreateUpdateEmployeeDto
{
    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    [Required]
    [Range(21, 100)]
    public int Age { get; set; } 

    [Required]
    public string Email { get; set; } 

    [Required]
    public float Salary { get; set; }
}
