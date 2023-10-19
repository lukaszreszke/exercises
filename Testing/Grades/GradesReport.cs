using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace Grades
{
    public class GradesReport
    {
        private const string ApiUrl = "https://api.example.com/grades";
        private const string XmlFilePath = "path/to/grades.xml";

        public async Task<List<StudentGrade>> GetGradesAsync()
        {
            var apiGrades = await GetGradesFromApiAsync();
            var xmlGrades = GetGradesFromXml();

            var allGrades = new List<StudentGrade>();
            var studentNames = await GetStudentNames();

            allGrades.AddRange(apiGrades.Select(g => new StudentGrade
            {
                FullName = studentNames.First(x => x.Key == g.StudentId).Value,
                Course = g.Course,
            }));
            allGrades.AddRange(xmlGrades.Select(g => new StudentGrade
            {
                FullName = g.FullName,
                Course = g.Subject,
                Grade = ConvertEvaluationToGrade(g.Evaluation)
            }));

            return allGrades;
        }

        private async Task<Dictionary<Guid, string>> GetStudentNames()
        {
            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync(ApiUrl);
                return JsonConvert.DeserializeObject<Dictionary<Guid, string>>(json);
            }
        }

        private async Task<List<ApiStudentGrade>> GetGradesFromApiAsync()
        {
            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync(ApiUrl);
                return JsonConvert.DeserializeObject<List<ApiStudentGrade>>(json);
            }
        }

        private List<XmlStudentGrade> GetGradesFromXml()
        {
            var doc = XDocument.Load(XmlFilePath);
            return doc.Descendants("Grade")
                .Select(g => new XmlStudentGrade
                {
                    FullName = (string)g.Element("FullName"),
                    Subject = (string)g.Element("Subject"),
                    Evaluation = (string)g.Element("Evaluation")
                })
                .ToList();
        }

        private double ConvertEvaluationToGrade(string evaluation)
        {
            switch (evaluation)
            {
                case "A+": return 4.5;
                case "A": return 4.0;
                case "B+": return 3.5;
                case "B": return 3.0;
                case "C+": return 2.5;
                case "C": return 2.0;
                case "D+": return 1.5;
                case "D": return 1.0;
                default: return 0.0;
            }
        }
    }
}