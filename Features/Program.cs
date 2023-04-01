using System;

namespace Features // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee { Id = 1, Name = "Scott" },
                new Employee { Id = 2, Name = "Chris" }
            };

            IEnumerable<Employee> sales = new List<Employee>()
            {
                new Employee {Id = 3, Name = "Alex"}
            };

            Console.WriteLine(developers.Count());
            IEnumerator<Employee> ennumerator = developers.GetEnumerator();
            while (ennumerator.MoveNext())
            {
                Console.WriteLine(ennumerator.Current.Name);
            }
        }
    }
}