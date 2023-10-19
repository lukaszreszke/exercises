namespace Grades;

public class ApiStudentGrade
{
    public Guid StudentId { get; set; }
    public string Course { get; set; }
    public int Points { get; set; } // Grade as points (for example 850/1000) 
}