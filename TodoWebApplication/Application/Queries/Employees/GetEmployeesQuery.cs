using MediatR;
using TodoWebApplication.Domain.Entities;
using TodoWebApplication.Domain.Interfaces;

namespace TodoWebApplication.Application.Queries.Employees;

public record GetEmployeesQuery : IRequest<IEnumerable<GetEmployeeDto>>;

public class GetEmployeesQueryHandle : IRequestHandler<GetEmployeesQuery, IEnumerable<GetEmployeeDto>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeesQueryHandle(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<GetEmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var employees = await _employeeRepository.GetAllSync();
        return employees.Select(
            employee => new GetEmployeeDto(employee.Id, employee.Name, employee.Position, employee.Level));
    }
}

public record GetEmployeesQueryById(int Id) : IRequest<GetEmployeeDto>;

public class GetEmployeeByIdQueryQuery : IRequestHandler<GetEmployeesQueryById, GetEmployeeDto>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeeByIdQueryQuery(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<GetEmployeeDto> Handle(GetEmployeesQueryById request, CancellationToken cancellationToken)
    {
        Employee employee = await _employeeRepository.GetByIdAsync(request.Id);
        if (employee == null)
        {
            throw new KeyNotFoundException($"Employee with ID {request.Id} not found.");
        }
        return new GetEmployeeDto(employee.Id, employee.Name, employee.Position, employee.Level);
    }
}


