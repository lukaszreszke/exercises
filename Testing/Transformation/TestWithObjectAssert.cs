namespace Transformation;

public class TestWithObjectAssert
{
    [Fact]
    public void TransformUserToEmployee_ShouldCreateCorrectEmployee()
    {
        var user = new User 
        {
            UserName = "Jan Kowalski",
            Email = "jan.kowalski@example.com",
            DateOfBirth = new DateTime(1985, 1, 1)
        };

        var employee = new Transform().TransformUserToEmployee(user);

        new EmployeeAssert(employee)
            .HasFullName("Jan Kowalski")
            .HasWorkEmail("jan.kowalski@example.com")
            .HasAge(38);
    }
}

public class EmployeeAssert
{
    private readonly Employee _employee;

    public EmployeeAssert(Employee employee)
    {
        _employee = employee;
    }

    public EmployeeAssert HasFullName(string expectedFullName)
    {
        Assert.Equal(expectedFullName, _employee.FullName);
        return this;
    }

    public EmployeeAssert HasWorkEmail(string expectedWorkEmail)
    {
        Assert.Equal(expectedWorkEmail, _employee.WorkEmail);
        return this;
    }

    public EmployeeAssert HasAge(int expectedAge)
    {
        Assert.Equal(expectedAge, _employee.Age);
        return this;
    }
}
