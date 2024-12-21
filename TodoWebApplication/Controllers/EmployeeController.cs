using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoWebApplication.Application.Commands.Employees;
using TodoWebApplication.Application.Queries.Employees;
using TodoWebApplication.Domain.Entities;

namespace TodoWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employeesDto = await _mediator.Send(new GetEmployeesQuery());
            return Ok(employeesDto);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employeesDto = await _mediator.Send(new GetEmployeesQueryById(id));
            return employeesDto == null ? NotFound() : Ok(employeesDto);

        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            var employeeId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeId }, employeeId);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(new { Message = "ID in route does not match ID in body." });
            }
            var result = await _mediator.Send(command);
            if (!result)
            {
                return NotFound(new { Message = $"Employee with ID {id} not found." });
            }

            return NoContent(); // HTTP 204
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeById(int id)
        {
            var result = await _mediator.Send(new DeleteEmployeeCommand(id));
            if (!result)
            {
                return NotFound(new { Message = $"Employee with ID {id} not found." });
            }
            return NoContent();

        }

    }
}
