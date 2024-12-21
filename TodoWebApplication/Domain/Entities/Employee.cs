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
        public int Level { get; set; }

        public Employee(string name, string position, int level)
        {
            Name = name;
            Position = position;
            Level = level;
        }


    }
}
