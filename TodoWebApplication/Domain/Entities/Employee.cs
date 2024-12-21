using TodoWebApplication.Domain.Enums;

namespace TodoWebApplication.Domain.Entities
{
    public class Employee
    {
        public Employee()
        {
        }

        public int Id { get; set; }
        public string Name { get; init; } 
        public string Position { get; set; }
        public EmployeeLevel Level { get; set; }

        public Employee(string name, string position, EmployeeLevel level)
        {
            Name = name;
            Position = position;
            Level = level;
        }


    }
}
