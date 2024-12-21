using MediatR;
using TodoWebApplication.Domain.Interfaces;
using TodoWebApplication.Domain.Entities;

namespace TodoWebApplication.Application.Commands.Employees;
public record CreateEmployeeCommand(string Name, string Position, int Level) : IRequest<int>;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly IEmployeeRepository _employeeRepository;

    public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        Employee employee = new Employee(request.Name, request.Position, request.Level);
        return await _employeeRepository.AddAsync(employee);
    }
}

