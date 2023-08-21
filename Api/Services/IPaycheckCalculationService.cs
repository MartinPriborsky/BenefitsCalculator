using Api.Dtos.Employee;
using Api.Dtos.Paychecks;

namespace Api.Services
{
    public interface IPaycheckCalculationService
    {
        PaycheckCalculationResult CalculatePaycheck(GetEmployeeDto employee);
    }
}
