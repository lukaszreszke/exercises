namespace Salaries.Domain;

public class Benefit
{
    public Benefit()
    {
        // Needed because of EF 
    }

    public Benefit(decimal value, string name)
    {
        Value = value;
        Name = name;
    }
    
    public int Id { get; private set;  }
    public decimal Value { get; private set;  }
    public string Name { get; private set; }
}