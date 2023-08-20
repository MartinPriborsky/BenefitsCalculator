using Api.Dtos.Dependent;

namespace Api.Repositories
{
    // Represents a repository interface for working with dependent data
    public interface IDependentRepository
    {
        Task<List<GetDependentDto>> GetAllDependents(CancellationToken cancellationToken);
        Task<GetDependentDto> GetDependentById(int id, CancellationToken cancellationToken);
    }
}
