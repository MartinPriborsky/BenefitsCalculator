using Api.Dtos.Employee;
using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeesController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id, CancellationToken cancellationToken)
    {
        try
        {
            var employee = await _employeeRepository.GetEmployeeById(id, cancellationToken);

            if (employee == null)
            {
                return NotFound();
            }

            return new ApiResponse<GetEmployeeDto>
            {
                Data = employee,
                Success = true
            };
        }
        catch(Exception ex)
        {
            return new ApiResponse<GetEmployeeDto>
            {
                Data = null,
                Success = false,
                Error = "Error while retrieving employee.",
                Message = ex.Message
            };
        }
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll(CancellationToken cancellationToken)
    {
        //task: use a more realistic production approach
        //solution: I will use an in-memory database to simulate the production database.
        //plus: I also added cancellationTokens, and repositories for accessing data.
        try
        {
            var employees = await _employeeRepository.GetAllEmployees(cancellationToken);

            return new ApiResponse<List<GetEmployeeDto>>
            {
                Data = employees,
                Success = true
            };
        }
        catch(Exception ex)
        {
            return new ApiResponse<List<GetEmployeeDto>>
            {
                Data = null,
                Success = false,
                Error = "Error while retrieving employees.",
                Message = ex.Message
            };
        }
    }
}