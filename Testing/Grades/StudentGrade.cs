namespace Grades;

public class StudentGrade
{
    public string FullName { get; set; }
    public string Course { get; set; }
    public double Grade { get; set; }

    public List<StudentGrade> From(List<ApiStudentGrade> apiStudentGrade)
    {
        return apiStudentGrade.Select(x => new StudentGrade
            {
                FullName = $"#{x.FirstName} #{x.LastName}", Course = x.Course,
                Grade = new PointGradeConverter().Get(x.Points)
            })
            .ToList();
    }

    public List<StudentGrade> From(List<XmlStudentGrade> xmlStudentGrades)
    {
        return xmlStudentGrades.Select(x => new StudentGrade
        {
            FullName = x.FullName,
            Course = x.Subject,
            Grade = new GradeConverter().Get(x.Evaluation)
        }).ToList();
    }
}

public class PointGradeConverter
{
    public double Get(int points)
    {
        return points switch
        {
            > 900 => 5,
            > 750 => 4.5,
            > 650 => 4,
            > 570 => 3.5,
            > 500 => 3,
            _ => 2
        };
    }
}

public class GradeConverter
{
    public double Get(string evaluation)
    {
        return evaluation switch
        {
            "A+" => 4.5,
            "A" => 4.0,
            "B+" => 3.5,
            "B" => 3.0,
            "C+" => 2.5,
            "C" => 2.0,
            "D+" => 1.5,
            "D" => 1.0,
            _ => 0.0
        };
    }
}