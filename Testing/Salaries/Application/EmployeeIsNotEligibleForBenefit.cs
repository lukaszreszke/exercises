namespace Salaries.Application;

public class EmployeeIsNotEligibleForBenefit : Exception
{
    public int EmployeeId { get; }

    public EmployeeIsNotEligibleForBenefit(int employeeId)
    {
        EmployeeId = employeeId;
    }
}