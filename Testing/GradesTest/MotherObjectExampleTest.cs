namespace Grades;

public class MotherObjectExampleTest
{
    [Fact]
    public void Mother_object_example()
    {
        var grade = ApiStudentGradeMotherObject.ApiStudentGrade().WithPoints(100);
        
        Assert.Equal(100, grade.Points);
    }
}