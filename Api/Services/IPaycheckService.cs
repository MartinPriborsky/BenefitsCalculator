using Api.Models;

namespace Api.Services
{
    public interface IPaycheckService
    {
        Task<List<Paycheck>> GetPaychecks(int employeeId, CancellationToken cancellationToken);
        Task<Paycheck> GetPaycheck(int employeeId, int paycheck, CancellationToken cancellationToken);
    }
}
