namespace Grades;

public static class ApiStudentGradeMotherObject
{
   public static ApiStudentGrade ApiStudentGrade()
   {
      return new ApiStudentGrade { FirstName = "Jan", LastName = "Kowalski", Course = "Matematyka", Points = 0};
   }
}

public static class ApiStudentGradeMotherObjectExtensions
{
   public static ApiStudentGrade WithPoints(this ApiStudentGrade grade, int points)
   {
      grade.Points = points;
      return grade;
   }
}