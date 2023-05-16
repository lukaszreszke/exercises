namespace EmployeesProjects;

public class UnitTest1
{
    [Fact]
    public void Employee_WithNoProjects_ShouldNotReceiveBonus()
    {
        var employee = new Employee
        {
            Name = "Jan Kowalski",
            Department = "IT",
            Salary = 5000,
            Projects = new List<Project>()
        };

        Assert.False(employee.ShouldReceiveBonus());
    }

    [Fact]
    public void Employee_WithProjects_ShouldReceiveBonus()
    {
        var employee = new Employee
        {
            Name = "Jan Kowalski",
            Department = "IT",
            Salary = 5000,
            Projects = new List<Project>
            {
                new Project
                {
                    Name = "Project 1", StartDate = DateTime.Now.AddMonths(-3), EndDate = DateTime.Now.AddMonths(3)
                },
                new Project
                {
                    Name = "Project 2", StartDate = DateTime.Now.AddMonths(-1), EndDate = DateTime.Now.AddMonths(2)
                }
            }
        };

        Assert.True(employee.ShouldReceiveBonus());
    }
}