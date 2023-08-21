using Api.Models;
using Api.Repositories;

namespace Api.Services
{
    public class PaycheckService : IPaycheckService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPaycheckCalculationService _paycheckCalculationService;

        public PaycheckService(IEmployeeRepository employeeRepository, IPaycheckCalculationService paycheckCalculationService)
        {
            _employeeRepository = employeeRepository;
            _paycheckCalculationService = paycheckCalculationService;
        }

        public async Task<Paycheck?> GetPaycheck(int employeeId, int paycheckId, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeById(employeeId, cancellationToken);
            if (employee == null)
            {
                return null;
            }

            var calculationResult = _paycheckCalculationService.CalculatePaycheck(employee);

            return new Paycheck
            {
                PaycheckId = paycheckId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Pay = calculationResult.Pay + AddRemaindingCentToLatePaychecks(paycheckId, calculationResult.Remainder)
            };
        }

        public async Task<List<Paycheck>> GetPaychecks(int employeeId, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeById(employeeId, cancellationToken);
            if (employee == null)
            {
                return new List<Paycheck>();
            }

            var calculationResult = _paycheckCalculationService.CalculatePaycheck(employee);

            List<Paycheck> paychecks = new();

            for (int paycheckId = 1; paycheckId <= GlobalConstants.NUMBER_OF_PAYCHECKS_PER_YEAR; paycheckId++)
            {
                paychecks.Add(new Paycheck
                {
                    PaycheckId = paycheckId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Pay = calculationResult.Pay + AddRemaindingCentToLatePaychecks(paycheckId, calculationResult.Remainder)
                });
            }

            return paychecks;
        }

        // Add one cent to last payments at the end of the year
        // It seems better, then give them prematurely
        private static decimal AddRemaindingCentToLatePaychecks(int paycheckId, int remainder)
        {
            return GlobalConstants.NUMBER_OF_PAYCHECKS_PER_YEAR - paycheckId < remainder ? 0.01m : 0.00m;
        }
    }
}
