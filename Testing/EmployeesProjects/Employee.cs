namespace EmployeesProjects;

public class Employee
{
    public string Name { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }
    public List<Project> Projects { get; set; }

    public bool ShouldReceiveBonus()
    {
        return Projects.Any();
    }
}

public class Project
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
