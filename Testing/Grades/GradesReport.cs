using Newtonsoft.Json;
using System.Net;

namespace Grades
{
    public class GradesReport
    {
        private const string ApiUrl = "https://api.example.com/grades";

        public async Task<List<StudentGrade>> GetGradesAsync()
        {
            var apiGrades = await GetGradesFromApiAsync();

            var allGrades = new List<StudentGrade>();
            allGrades.AddRange(apiGrades.Select(g => new StudentGrade
            {
                FullName = $"{g.FirstName} {g.LastName}",
                Course = g.Course,
                Grade = GradeFromPoints(g.Points) // Transform from points to grade
            }));

            return allGrades;
        }

        private static double GradeFromPoints(int points)
        {
            if (points >= 90 && points <= 100)
            {
                return 5.0;
            }
            else if (points >= 80 && points < 90)
            {
                return 4.5;
            }
            else if (points >= 70 && points < 80)
            {
                return 4.0;
            }
            else if (points >= 60 && points < 70)
            {
                return 3.5;
            }
            else if (points >= 50 && points < 60)
            {
                return 3.0;
            }
            else if (points >= 45 && points < 50)
            {
                return 2.5;
            }
            else return 2.0;
        }

        private async Task<List<ApiStudentGrade>> GetGradesFromApiAsync()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(ApiUrl);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new InvalidResponseException();
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ApiStudentGrade>>(json);
        }
    }

    public class InvalidResponseException : Exception
    {
    }
}