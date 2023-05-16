namespace Transformation;

public class UnitTest1
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

        Assert.Equal("Jan Kowalski", employee.FullName);
        Assert.Equal("jan.kowalski@example.com", employee.WorkEmail);
        Assert.Equal(38, employee.Age);
    }

}