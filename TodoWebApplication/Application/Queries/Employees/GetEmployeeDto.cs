namespace TodoWebApplication.Application.Queries.Employees
{
    public class GetEmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public string Position { get; set; }
        public int Level { get; set; }

        public GetEmployeeDto()
        {
        }

        public GetEmployeeDto(int id, string name, string position, int level)
        {
            Id = id;
            Name = name;
            Position = position;
            Level = level;
        }
    }
}
