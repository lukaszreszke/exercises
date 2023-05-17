using Salaries.Infra;

namespace Salaries.Application;

public class BenefitService
{
    private readonly SalariesContext _salariesContext;

    public BenefitService(SalariesContext salariesContext)
    {
        _salariesContext = salariesContext;
    }

    public void CreateBenefit(string name, decimal value)
    {
        
    }

    public void AddBenefitForEmployee(int employeeId, int benefitId)
    {
        var employee = _salariesContext.Employees.Find(employeeId);
        if (employee == null) throw new EmployeeNotFound(employeeId);
        var benefit = _salariesContext.Benefits.Find(benefitId);
        if (benefit == null) throw new BenefitNotFound(benefitId);

        if (employee.Grade == 3 && employee.Benefits.Any() &&
            employee.Benefits.Sum(x => x.Value) + benefit.Value < 10000)
        {
            employee.Benefits.Add(benefit);
        }
        else if (employee.Grade == 2 && employee.Benefits.Any() && employee.Benefits.Count < 5 &&
                 employee.Benefits.Sum(x => x.Value) + benefit.Value < 6000)
        {
            employee.Benefits.Add(benefit);
        }
        else if (employee.Grade == 1 && employee.Benefits.Any() && employee.Benefits.Count < 5)
        {
            if (employee.PromotedAt <= DateTime.UtcNow.AddYears(-3) &&
                employee.Benefits.Sum(x => x.Value) + benefit.Value < 3000)
            {
                employee.Benefits.Add(benefit);
            }
            else if (employee.Benefits.Sum(x => x.Value) + benefit.Value < 1500)
            {
                employee.Benefits.Add(benefit);
            }
        }
        else
        {
            throw new EmployeeIsNotEligibleForBenefit(employeeId);
        }

        _salariesContext.SaveChanges();
    }
}