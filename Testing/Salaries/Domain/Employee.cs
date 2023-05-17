namespace Salaries.Domain;

public class Employee
{
    public Employee()
    {
        // EF
    }

    public Employee(string firstName, string lastName, int grade = 1)
    {
        FirstName = firstName;
        LastName = lastName;
        Grade = 1;
        Benefits = new List<Benefit>();
    }
    
    public int Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    
    public Salary? Salary { get; private set; }
    public List<Benefit> Benefits { get; private set; }
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
}