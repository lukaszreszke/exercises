namespace Salaries.Domain;

public class Salary
{
    public Salary()
    {
       // Needed because of EF 
    }
    
    public int Id { get; private set;  }
    public decimal Amount { get; private set;  }
}