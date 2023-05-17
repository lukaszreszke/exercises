using Salaries.Domain;
using Salaries.Infra;

namespace Salaries.Application;

public class EmployeeService
{
    private readonly SalariesContext _salariesContext;

    public EmployeeService(SalariesContext salariesContext)
    {
        _salariesContext = salariesContext;
    }

    public Employee CreateEmployee(string firstName, string lastName)
    {
        var employee = new Employee(firstName, lastName);
        _salariesContext.Add(employee);
        _salariesContext.SaveChanges();
        return employee;
    }

    public TotalCompensation GetTotalEmployeeCompensation(int employeeId)
    {
        return new TotalCompensation(0, 0);
    }
}

public class TotalCompensation
{
    private readonly decimal _salary;
    private readonly decimal _benefitsValue;

    public TotalCompensation(decimal salary, decimal benefitsValue)
    {
        _salary = salary;
        _benefitsValue = benefitsValue;
    }

    public decimal Get() => _salary + _benefitsValue;
}