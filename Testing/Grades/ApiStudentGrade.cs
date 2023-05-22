namespace Grades;

public class ApiStudentGrade
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Course { get; set; }
    public int Points { get; set; } // Grade as points (ie. 850/1000)
}