using System.Text.Json.Serialization;
using TodoWebApplication.Domain.Enums;

namespace TodoWebApplication.Application.Queries.Employees
{
    public class EmployeeWithTasksDto
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public string Position { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]  // Chuyển Enum thành string khi serialize
        public EmployeeLevel Level { get; set; }
        public List<TaskDetailDto> TaskDetails { get; set; }

        public EmployeeWithTasksDto()
        {
        }

        public EmployeeWithTasksDto(int id, string name, string position, EmployeeLevel level, List<TaskDetailDto> taskDetails)
        {
            Id = id;
            Name = name;
            Position = position;
            Level = level;
            TaskDetails = taskDetails;
        }
    }
}
