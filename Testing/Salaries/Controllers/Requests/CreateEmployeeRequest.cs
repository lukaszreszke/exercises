namespace Salaries.Controllers;

public class CreateEmployeeRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string InMarketSince { get; set; }
    public string? Salary { get; set; }
}