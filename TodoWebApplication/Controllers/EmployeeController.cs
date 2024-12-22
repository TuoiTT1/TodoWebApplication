using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TodoWebApplication.Application.Commands.Employees;
using TodoWebApplication.Application.Queries.Employees;

namespace TodoWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IMediator mediator, ILogger<EmployeeController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                _logger.LogInformation("Getting all employees");
                var employees = await _mediator.Send(new GetEmployeesQuery());
                if (employees.IsNullOrEmpty())
                {
                    _logger.LogWarning("No employees found.");
                    return NotFound("No employees available.");
                }
                _logger.LogInformation("Successfully retrieved {Count} employees.", employees.Count());
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all employees.");
                return StatusCode(500, new { Message = "An error occurred while getting all employees." });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            _logger.LogInformation($"Getting employee with ID {id}");
            var employeesDto = await _mediator.Send(new GetEmployeesQueryById(id));
            if (employeesDto == null)
            {
                _logger.LogWarning($"Employee with ID {id} not found.");
                return NotFound(new { Message = $"Employee with ID {id} not found." });
            }
            else
            {
                _logger.LogInformation($"Employee with ID {id} found.");
                return Ok(employeesDto);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            _logger.LogInformation("Creating employee");
            try
            {
                var employeeId = await _mediator.Send(command);
                _logger.LogInformation("Successfully created employee: {@Result}.", employeeId);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeId }, employeeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating employee.");
                return StatusCode(500, new { Message = "An error occurred while creating employee." });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeCommand command)
        {
            _logger.LogInformation($"Updating employee with ID {id}");
            if (id != command.Id)
            {
                return BadRequest(new { Message = "ID in route does not match ID in body." });
            }
            try
            {
                var result = await _mediator.Send(command);

                _logger.LogInformation($"Successfully updated employee with ID {id}.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating employee.");
                return StatusCode(500, new { Message = "An error occurred while updating employee." });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeById(int id)
        {
            _logger.LogInformation($"Deleting employee with ID {id}");
            try
            {
                var result = await _mediator.Send(new DeleteEmployeeCommand(id));
                if (!result)
                {
                    _logger.LogWarning($"Employee with ID {id} not found.");
                    return NotFound(new { Message = $"Employee with ID {id} not found." });
                }
                _logger.LogInformation($"Successfully deleted employee with ID {id}.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting employee.");
                return StatusCode(500, new { Message = "An error occurred while deleting employee." });
            }

        }

        [HttpGet("{employeeId}/tasks")]
        public async Task<IActionResult> GetTasksByEmployeeId(int employeeId)
        {
            _logger.LogInformation($"Getting tasks for employee with ID {employeeId}");
            var result = await _mediator.Send(new GetListEmployeeTaskListQuery(employeeId));
            if (result == null)
            {
                _logger.LogWarning($"No tasks found for employee with ID {employeeId}");
                return NotFound(new { Message = $"No tasks found for employee with ID {employeeId}" });
            }
             _logger.LogInformation($"Successfully retrieved tasks for employee with ID {employeeId}: {result}");
            return Ok(result);
        }
    }
}
