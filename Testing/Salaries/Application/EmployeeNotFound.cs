namespace Salaries.Application;

public class EmployeeNotFound : Exception
{
    public int EmployeeId { get; }

    public EmployeeNotFound(int employeeId)
    {
        EmployeeId = employeeId;
    }
}