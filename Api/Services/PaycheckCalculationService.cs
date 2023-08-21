using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;

namespace Api.Services
{
    public class PaycheckCalculationService : IPaycheckCalculationService
    {
        public (decimal, int) CalculatePaycheck(GetEmployeeDto employee)
        {
            // Calculate the cost of benefits
            decimal baseCost = GetBaseCost();
            decimal dependentsCost = GetDependentsCost(employee.Dependents);
            decimal dependentsOverFiftyCost = GetDependentsOverFiftyCost(employee.Dependents);
            decimal over80kCost = GetOver80kCost(employee.Salary);

            // Calculate the yearly salary after deducting benefit costs
            var yearlySalaryAfterBenefits = employee.Salary - baseCost - dependentsCost - dependentsOverFiftyCost - over80kCost;

            // Calculate bi-weekly pay by dividing the yearly salary by the number of paychecks per year
            // Round to two decimal places, rounding down
            var biWeeklyPay = Math.Round(yearlySalaryAfterBenefits / GlobalConstants.NUMBER_OF_PAYCHECKS_PER_YEAR, 2, MidpointRounding.ToZero);

            // Calculate the remainder in cents to distribute evenly among paychecks
            // Convert the remainder to an integer value
            int remainder = (int)((yearlySalaryAfterBenefits - (biWeeklyPay * GlobalConstants.NUMBER_OF_PAYCHECKS_PER_YEAR)) * 100);

            // Return the calculated bi-weekly pay and remainder in cents
            return (biWeeklyPay, remainder);
        }

        private static decimal GetBaseCost()
        {
            return GlobalConstants.BASE_COST_PER_YEAR;
        }

        private static decimal GetDependentsCost(ICollection<GetDependentDto> dependents)
        {
            return GetDependentsWithRelationship(dependents).Count * GlobalConstants.ONE_DEPENDENT_COST_PER_YEAR;
        }

        private static decimal GetDependentsOverFiftyCost(ICollection<GetDependentDto> dependents)
        {
            return GetDependentsWithRelationship(GetDependentsOverFifty(dependents)).Count * GlobalConstants.ONE_DEPENDENT_OVER_FIFTY_ADDITIONAL_COST_PER_YEAR;
        }

        private static decimal GetOver80kCost(decimal salary)
        {
            return HasOver80kSalery(salary) ? salary * GlobalConstants.PROCENT_OF_REDUCTION_IF_OVER_80K : 0;
        }

        // Returns a list of Dependents without "None" Relationships
        private static List<GetDependentDto> GetDependentsWithRelationship(ICollection<GetDependentDto> dependents)
        {
            List<GetDependentDto> dependentsOverFifty = new();

            foreach (var dependent in dependents)
            {
                if (dependent.Relationship != Relationship.None)
                {
                    // Include dependents without None Relationships in the list
                    dependentsOverFifty.Add(dependent);
                }
            }
            return dependentsOverFifty;
        }

        // Returns a list of Dependents who are over 50 years old
        private static List<GetDependentDto> GetDependentsOverFifty(ICollection<GetDependentDto> dependents)
        {
            List<GetDependentDto> dependentsOverFifty = new() { };

            foreach (var dependant in dependents)
            {
                // Check if the dependent is over 50 years old
                // Note: This can change daily, in a real scenario, generate once and store in the database.
                if (dependant.DateOfBirth < DateTime.Now.AddYears(-50))
                {
                    // Include dependents over 50 years old in the list
                    dependentsOverFifty.Add(dependant);
                }
            }
            return dependentsOverFifty;
        }

        // Returns true if yearly salary is over $80,000
        private static bool HasOver80kSalery(decimal salery)
        {
            return salery > 80000.00m;
        }
    }
}
