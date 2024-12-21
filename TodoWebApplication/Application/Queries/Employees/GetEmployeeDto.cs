using System.Text.Json.Serialization;
using TodoWebApplication.Domain.Enums;

namespace TodoWebApplication.Application.Queries.Employees
{
    public class GetEmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public string Position { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]  // Chuyển Enum thành string khi serialize
        public EmployeeLevel Level { get; set; }

        public GetEmployeeDto()
        {
        }

        public GetEmployeeDto(int id, string name, string position, EmployeeLevel level)
        {
            Id = id;
            Name = name;
            Position = position;
            Level = level;
        }
    }
}
