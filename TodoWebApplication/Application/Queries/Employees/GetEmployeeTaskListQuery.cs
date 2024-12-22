using MediatR;
using TodoWebApplication.Domain.Interfaces;

namespace TodoWebApplication.Application.Queries.Employees;

public record GetListEmployeeTaskListQuery(int Id) : IRequest<EmployeeWithTasksDto>;
public class GetListEmployeeTaskListQueryHanlder : IRequestHandler<GetListEmployeeTaskListQuery, EmployeeWithTasksDto>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetListEmployeeTaskListQueryHanlder(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public Task<EmployeeWithTasksDto> Handle(GetListEmployeeTaskListQuery request, CancellationToken cancellationToken)
    {
        return _employeeRepository.GetEmployeeWithTasksAsync(request.Id);
    }
}

