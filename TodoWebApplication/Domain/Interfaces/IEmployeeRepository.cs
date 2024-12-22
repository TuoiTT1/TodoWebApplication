using TodoWebApplication.Application.Queries.Employees;
using TodoWebApplication.Domain.Entities;

namespace TodoWebApplication.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllSync();
        Task<Employee> GetByIdAsync(int id);
        Task<int> AddAsync(Employee employee);
        Task<bool> UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(int id);
        Task<EmployeeWithTasksDto> GetEmployeeWithTasksAsync(int id);

    }
}
