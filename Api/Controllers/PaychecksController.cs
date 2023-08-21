using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PaychecksController : ControllerBase
{
    private readonly IPaycheckService _paycheckService;

    public PaychecksController(IPaycheckService paycheckService)
    {
        _paycheckService = paycheckService;
    }

    [SwaggerOperation(Summary = "Get all paychecks by employeeId")]
    [HttpGet("{employeeId}")]
    public async Task<ActionResult<ApiResponse<List<Paycheck>>>> GetAll(int employeeId, CancellationToken cancellationToken)
    {
        var paycheck = await _paycheckService.GetPaychecks(employeeId, cancellationToken);

        if (paycheck.Count == 0)
        {
            return NotFound();
        }

        return new ApiResponse<List<Paycheck>>
        {
            Data = paycheck,
            Success = true
        };
    }

    [SwaggerOperation(Summary = "Get paycheck by employeeId and paycheckId")]
    [HttpGet("{employeeId}/{paycheckId}")]
    public async Task<ActionResult<ApiResponse<Paycheck>>> Get(int employeeId, int paycheckId, CancellationToken cancellationToken)
    {
        if (paycheckId < 1 || paycheckId > 26)
        {
            throw new Exception("Invalid paycheckId. It should be between 1 and 26.");
        }

        var paycheck = await _paycheckService.GetPaycheck(employeeId, paycheckId, cancellationToken);

        if (paycheck == null)
        {
            return NotFound();
        }

        return new ApiResponse<Paycheck>
        {
            Data = paycheck,
            Success = true
        };
    }
}
