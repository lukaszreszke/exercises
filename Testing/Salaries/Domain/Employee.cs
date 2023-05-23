namespace Salaries.Domain;

public class Employee
{
    private Employee()
    {
        // EF
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
}