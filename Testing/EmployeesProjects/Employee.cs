using System.Runtime.CompilerServices;

namespace EmployeesProjects;

public class Employee
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }
    public List<Project> Projects { get; set; } = new List<Project>();

    public bool ShouldReceiveBonus()
    {
        return Projects.Any();
    }
}

public static class ObjectMother {
    public static Employee WithName(this Employee employee, string name)
    {
        employee.FirstName = name.Split(" ")[0];
        employee.LastName = name.Split(" ")[1];
        return employee;
    }

    public static Employee WithSalary(this Employee employee, decimal salary)
    {
        employee.Salary = salary;
        return employee;
    }

    public static Employee WithProject(this Employee employee, Project project)
    {
        employee.Projects.Add(project);
        return employee;
    }

    public static Employee InDepartment(this Employee employee, string department)
    {
        employee.Department = department;
        return employee;
    }
}

public class Project
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
