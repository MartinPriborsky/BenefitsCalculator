using Api.Models;

namespace Api.Extensions
{
    public static class EmployeeExtension
    {
        // Requirement : an employee may only have 1 spouse or domestic partner (not both)
        // This requirement should be also checked when creating new Dependent! (but not part of this task)
        public static void ValidateDependents(this Employee employee)
        {
            if(employee.Dependents.Count(d => d.Relationship == Relationship.Spouse || d.Relationship == Relationship.DomesticPartner) > 1)
            {
                throw new Exception($"An employee ID:{employee.Id} have more than one Spouse / Domestic Partner");
            }
        }
    }
}
