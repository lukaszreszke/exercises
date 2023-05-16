namespace Transformation;

public class Transform
{
    public Employee TransformUserToEmployee(User user)
    {
        var now = DateTime.Today;
        var age = now.Year - user.DateOfBirth.Year;
        if (user.DateOfBirth.Date > now.AddYears(-age)) age--;
        return new Employee
        {
            FullName = user.UserName,
            WorkEmail = user.Email,
            Age = age
        };
    }
}