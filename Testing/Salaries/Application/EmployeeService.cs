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

    public Employee CreateEmployee(string firstName, string lastName, DateTime inMarketSince, Money? salary = null)
    {
        var employee = new Employee(firstName, lastName, inMarketSince);

        employee.SetSalary(salary ?? ProposedSalary(inMarketSince));

        _salariesContext.Add(employee);
        _salariesContext.SaveChanges();
        return employee;
    }

    private static Money? ProposedSalary(DateTime inMarketSince)
    {
        decimal salary = Decimal.Zero;
        if (inMarketSince > DateTime.UtcNow.AddYears(-5))
        {
            salary = 15000;
        }
        else if (inMarketSince > DateTime.UtcNow.AddYears(-3))
        {
            salary = 10000;
        }
        else if (inMarketSince > DateTime.UtcNow.AddYears(-1))
        {
            salary = 5000;
        }

        return new Money(salary);
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