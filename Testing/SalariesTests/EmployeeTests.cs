using Salaries.Application;
using Salaries.Domain;

namespace SalariesTests;

public class EmployeeTests
{
    [Fact]
    public void grade_3_employee_can_have_benefits()
    {
        var employee = new Employee("Jan", "Kowalski", DateTime.UtcNow, 3);
        var benefit1 = new Benefit(5000, "Scooter");
        var benefit2 = new Benefit(4999, "Bike");
        
        employee.GrantBenefit(benefit1);
        employee.GrantBenefit(benefit2);

        Assert.Contains(benefit1, employee.Benefits);
        Assert.Contains(benefit2, employee.Benefits);
    }

    [Fact]
    public void grade_3_employee_cannot_have_benefits_that_exceed_acceptable_value()
    {
        var employee = new Employee("Jan", "Kowalski", DateTime.UtcNow, 3);
        var benefit1 = new Benefit(5000, "Scooter");
        var benefit2 = new Benefit(4999, "Bike");
        var benefit3 = new Benefit(1, "Candy");
        
        employee.GrantBenefit(benefit1);
        employee.GrantBenefit(benefit2);

        Assert.Throws<EmployeeIsNotEligibleForBenefit>(() => employee.GrantBenefit(benefit3));
    }

    [Fact]
    public void grade_2_employee_can_have_5_benefits_that_dont_exceed_6000()
    {
        var employee = new Employee("Jan", "Kowalski", DateTime.UtcNow, 2);
        var benefit = new Benefit(1000, "Something");
        
        employee.GrantBenefit(benefit);
        employee.GrantBenefit(benefit);
        employee.GrantBenefit(benefit);
        employee.GrantBenefit(benefit);
        employee.GrantBenefit(benefit);

        Assert.Contains(benefit, employee.Benefits);
        Assert.Equal(5, employee.Benefits.Count());
    }
    
    [Fact]
    public void grade_2_employee_cannot_have__more_than_5_benefits()
    {
        var employee = new Employee("Jan", "Kowalski", DateTime.UtcNow, 2);
        var benefit = new Benefit(1000, "Something");
        
        employee.GrantBenefit(benefit);
        employee.GrantBenefit(benefit);
        employee.GrantBenefit(benefit);
        employee.GrantBenefit(benefit);
        employee.GrantBenefit(benefit);

        Assert.Throws<EmployeeIsNotEligibleForBenefit>(() => employee.GrantBenefit(benefit));
    }
    
    [Fact]
    public void grade_2_employee_cannot_have_benefits_worth_more_than_6k()
    {
        var employee = new Employee("Jan", "Kowalski", DateTime.UtcNow, 2);
        var benefit = new Benefit(2001, "Something");
        
        employee.GrantBenefit(benefit);
        employee.GrantBenefit(benefit);

        Assert.Throws<EmployeeIsNotEligibleForBenefit>(() => employee.GrantBenefit(benefit));
    }
}