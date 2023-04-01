using System;

namespace EmpAssignment 
{
    public class Employee
    {
        private int Id { get; set; }
        private string Name { get; set; }
        private string DepartmentName { get; set; }

        public delegate void MethodCalledEventHandler(string message); //using delegate
        public event MethodCalledEventHandler MethodCalled;

        public Employee(int id, string name, string departmentName)
        {
            Id = id;
            Name = name;
            DepartmentName = departmentName;
        }

        public int GetId()
        {
            //When method called GetId() will print
            OnMethodCalled("GetId() method called");
            return Id;
        }

        public string GetName()
        {
            //GetName() will print
            OnMethodCalled("GetName() method called");
            return Name;
        }

        public string GetDepartmentName()
        {
            //GetDepartment will print
            OnMethodCalled("GetDepartmentName() method called");
            return DepartmentName;
        }

        public void UpdateEmployee(int id)
        {
            Id = id;
        }

        public void UpdateEmployee(string name)
        {
            Name = name;
        }

        public void UpdateDepartment(string departmentName)
        {
            DepartmentName = departmentName;
        }

        protected virtual void OnMethodCalled(string message)
        {
            if (MethodCalled != null)
                MethodCalled(message);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //User will enter details
            Console.WriteLine("Enter Emp ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Emp Name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Emp Department Name: ");
            string departmentName = Console.ReadLine();

            Employee emp = new Employee(id, name, departmentName);
            emp.MethodCalled += MethodCalledEvent;

            Console.WriteLine("Emp ID: " + emp.GetId());
            Console.WriteLine("Emp Name: " + emp.GetName());
            Console.WriteLine("Emp Department Name: " + emp.GetDepartmentName());

            //When you want to Update Employee Details
            Console.WriteLine("Update Employee Deatils: ");

            //Entering all the three values to upate the details
            Console.WriteLine("Enter updated id, name and Department Name: ");
            id = Convert.ToInt32(Console.ReadLine());
            name = Console.ReadLine();
            departmentName = Console.ReadLine();
            emp.UpdateEmployee(id); // Updating id
            emp.UpdateEmployee(name);// Updating name
            emp.UpdateDepartment(departmentName); // Updating department name

            Console.WriteLine("Updated Details are: ");// Printing Updating Details
            Console.WriteLine(emp.GetId() + " " + emp.GetName() + " " + emp.GetDepartmentName());


            static void MethodCalledEvent(string message)
            {
                Console.WriteLine("\n" + message);
            }
        }
    }
}