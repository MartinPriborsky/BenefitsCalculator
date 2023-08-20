using Api.Dtos.Dependent;
using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly IDependentRepository _dependentRepository;

    public DependentsController(IDependentRepository dependentRepository)
    {
        _dependentRepository = dependentRepository;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id, CancellationToken cancellationToken)
    {
        var dependent = await _dependentRepository.GetDependentById(id, cancellationToken);

        if (dependent == null)
        {
            return NotFound();
        }

        return new ApiResponse<GetDependentDto>
        {
            Data = dependent,
            Success = true
        };
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll(CancellationToken cancellationToken)
    {
        var dependents = await _dependentRepository.GetAllDependents(cancellationToken);

        return new ApiResponse<List<GetDependentDto>>
        {
            Data = dependents,
            Success = true
        };
    }
}
