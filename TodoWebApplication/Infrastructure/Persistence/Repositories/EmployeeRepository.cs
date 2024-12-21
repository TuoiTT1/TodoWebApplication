using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using TodoWebApplication.Domain.Entities;
using TodoWebApplication.Domain.Interfaces;

namespace TodoWebApplication.Infrastructure.Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbConnection _dbConnection;

        public EmployeeRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Employee>> GetAllSync()
        {
            const string sql = "SELECT Id, Name, Position, Level FROM Employee";
            return await _dbConnection.QueryAsync<Employee>(sql);
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            const string sql = "SELECT Id, Name, Position, Level FROM Employee WHERE Id = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<Employee>(sql, new { Id = id });
        }
        public async Task<int> AddAsync(Employee employee)
        {
            const string sql = "INSERT INTO Employee (Name, Position, Level) OUTPUT INSERTED.Id VALUES (@Name, @Position, @Level)";
            return await _dbConnection.ExecuteScalarAsync<int>(sql, employee);
        }

        public async Task<bool> UpdateAsync(Employee employee)
        {
            const string sql = "UPDATE Employee SET Name = @Name , Position = @Position, Level = @Level WHERE Id = @Id";
            return await _dbConnection.ExecuteAsync(sql, employee) > 0;
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = "DELETE FROM Employee WHERE Id = @Id";
            var result = await _dbConnection.ExecuteAsync(sql, new {Id = id});
            return result > 0;

        }
    }
}
