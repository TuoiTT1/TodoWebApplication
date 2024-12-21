using MediatR;
using TodoWebApplication.Domain.Entities;
using TodoWebApplication.Domain.Interfaces;

namespace TodoWebApplication.Application.Commands.Employees;

public record UpdateEmployeeCommand(int Id, string Name, string Position, int Level) : IRequest<bool>;
public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, bool>
{
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee
        {
            Id = request.Id,
            Name = request.Name,
            Position = request.Position,
            Level = request.Level
        };

        return await _employeeRepository.UpdateAsync(employee);
    }
}
