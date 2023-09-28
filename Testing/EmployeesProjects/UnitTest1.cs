namespace EmployeesProjects;

public class UnitTest1
{
    [Fact]
    public void Employee_WithNoProjects_ShouldNotReceiveBonus()
    {
        var employee = new Employee().WithName("Test Err").WithSalary(15000);
        Assert.False(employee.ShouldReceiveBonus());
    }

    [Fact]
    public void Employee_WithProjects_ShouldReceiveBonus()
    {
        var employee = new Employee().WithName("Test Orr").WithProject(new Project()).WithSalary(15000);
        Assert.True(employee.ShouldReceiveBonus());
    }
}