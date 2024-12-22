using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using TodoWebApplication.Application.Queries.Employees;
using TodoWebApplication.Domain.Entities;
using TodoWebApplication.Domain.Interfaces;
using TodoWebApplication.Infrastructure.Logging;

namespace TodoWebApplication.Infrastructure.Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(IDbConnection dbConnection, ILogger<EmployeeRepository> logger)
        {
            _dbConnection = dbConnection;
            _logger = logger;
        }

        public async Task<IEnumerable<Employee>> GetAllSync()
        {
            const string sql = "SELECT Id, Name, Position, Level FROM Employee";
            SqlLogger.LogQuery(_logger, sql, null);
            return await _dbConnection.QueryAsync<Employee>(sql);
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            const string sql = "SELECT Id, Name, Position, Level FROM Employee WHERE Id = @Id";
            SqlLogger.LogQuery(_logger, sql, new { Id = id });
            return await _dbConnection.QuerySingleOrDefaultAsync<Employee>(sql, new { Id = id });
        }
        public async Task<int> AddAsync(Employee employee)
        {
            const string sql = "INSERT INTO Employee (Name, Position, Level) OUTPUT INSERTED.Id VALUES (@Name, @Position, @Level)";
            SqlLogger.LogQuery(_logger, sql, employee);
            return await _dbConnection.ExecuteScalarAsync<int>(sql, employee);
        }

        public async Task<bool> UpdateAsync(Employee employee)
        {
            const string sql = "UPDATE Employee SET Name = @Name , Position = @Position, Level = @Level WHERE Id = @Id";
            SqlLogger.LogQuery(_logger, sql, employee);
            return await _dbConnection.ExecuteAsync(sql, employee) > 0;
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = "DELETE FROM Employee WHERE Id = @Id";
            SqlLogger.LogQuery(_logger, sql, new { Id = id });
            var result = await _dbConnection.ExecuteAsync(sql, new {Id = id});
            return result > 0;

        }

        public async Task<EmployeeWithTasksDto> GetEmployeeWithTasksAsync(int id)
        {
            const string sql = "SELECT " +
                                    "e.Id, " +
                                    "e.Name, " +
                                    "e.Position, " +
                                    "e.Level, " +
                                    "t.Id, " +
                                    "t.Name, " +
                                    "t.Description, " +
                                    "t.StartDate, " +
                                    "t.EndDate, " +
                                    "t.IsCompleted " +
                                "FROM Employee e " +
                                "LEFT JOIN TaskDetail t ON e.Id = t.EmployeeId " +
                                "WHERE e.Id = @Id";
            
            SqlLogger.LogQuery(_logger, sql, new { Id = id });
            var empDictionary = new Dictionary<int, EmployeeWithTasksDto>();
            await _dbConnection.QueryAsync<EmployeeWithTasksDto, TaskDetailDto, EmployeeWithTasksDto>(
                sql,
                (emp, task) =>
                {
                    if (!empDictionary.TryGetValue(emp.Id, out var empEntry))
                    {
                        empEntry = emp;
                        empEntry.TaskDetails = new List<TaskDetailDto>();
                        empDictionary.Add(emp.Id, empEntry);
                    }
                    if (task != null)
                    {
                        empEntry.TaskDetails.Add(task);
                    }
                    return empEntry;
                },
                new { Id = id }
            );
            return empDictionary.Values.FirstOrDefault();
        }
    }
}
