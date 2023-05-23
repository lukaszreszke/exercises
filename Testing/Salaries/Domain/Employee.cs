using Salaries.Application;

namespace Salaries.Domain;

public class Employee
{
    private Employee()
    {
        // EF
        Benefits = new List<Benefit>();
    }

    public Employee(string firstName, string lastName, DateTime inJobMarketSince, int grade = 1)
    {
        FirstName = firstName;
        LastName = lastName;
        InJobMarketSince = inJobMarketSince;
        Grade = grade;
        Benefits = new List<Benefit>();
    }

    public int Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime InJobMarketSince { get; private set; }

    public Money? Salary { get; private set; }
    public ICollection<Benefit> Benefits { get; internal set; }
    public int Grade { get; private set; }
    public DateTime PromotedAt { get; private set; }

    public void Promote()
    {
        if (Grade < 3)
        {
            Grade++;
            PromotedAt = DateTime.UtcNow;
        }
    }

    public void SetSalary(Money? amount)
    {
        Salary = amount;
    }

    public void GrantBenefit(Benefit benefit)
    {
        if (Grade == 3  &&
            Benefits.Sum(x => x.Value) + benefit.Value < 10000)
        {
            Benefits.Add(benefit);
        }
        else if (Grade == 2 && Benefits.Count < 5 &&
                 Benefits.Sum(x => x.Value) + benefit.Value < 6000)
        {
            Benefits.Add(benefit);
        }
        else if (Grade == 1 && Benefits.Count < 5)
        {
            if (PromotedAt <= DateTime.UtcNow.AddYears(-3) &&
                Benefits.Sum(x => x.Value) + benefit.Value < 3000)
            {
                Benefits.Add(benefit);
            }
            else if (Benefits.Sum(x => x.Value) + benefit.Value < 1500)
            {
                Benefits.Add(benefit);
            }
        }
        else throw new EmployeeIsNotEligibleForBenefit(Id);
    }
}