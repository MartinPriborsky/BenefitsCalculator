using Api.Dtos.Employee;

namespace Api.Services
{
    public interface IPaycheckCalculationService
    {
        (decimal, int) CalculatePaycheck(GetEmployeeDto employee);
    }
}
