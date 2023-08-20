using Api.Dtos.Employee;

namespace Api.Repositories
{
    // Represents a repository interface for working with employee data
    public interface IEmployeeRepository
    {
        Task<List<GetEmployeeDto>> GetAllEmployees(CancellationToken cancellationToken);
        Task<GetEmployeeDto> GetEmployeeById(int id, CancellationToken cancellationToken);
    }
}
