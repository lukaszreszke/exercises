using System.Reflection;
using System.Xml.Linq;

namespace Grades;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void GradesXmlLoader()
    {
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../grades.xml");
        var expected = new List<XmlStudentGrade>
        {
            new() { FullName = "John Doe", Subject = "English", Evaluation = "A"},
            new() { FullName = "Jane Doe", Subject = "History", Evaluation = "B+"},
            new() { FullName = "Bob Smith", Subject = "Art", Evaluation = "C+"},
        };

        Assert.IsTrue(expected.SequenceEqual(new GradesXmlLoader().Load(filePath), new XmlStudentGradeEqualityComparer()));
    }
}


public class XmlStudentGradeEqualityComparer : IEqualityComparer<XmlStudentGrade>
{
    public bool Equals(XmlStudentGrade x, XmlStudentGrade y)
    {
        if (ReferenceEquals(x, y))
            return true;

        if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            return false;

        // Compare the properties of XmlStudentGrade for equality
        return x.FullName == y.FullName &&
               x.Subject == y.Subject &&
               x.Evaluation == y.Evaluation;
    }

    public int GetHashCode(XmlStudentGrade obj)
    {
        // Generate a hash code based on the properties of XmlStudentGrade
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + (obj.FullName?.GetHashCode() ?? 0);
            hash = hash * 23 + (obj.Subject?.GetHashCode() ?? 0);
            hash = hash * 23 + (obj.Evaluation?.GetHashCode() ?? 0);
            return hash;
        }
    }
}

public class GradesXmlLoader
{
    public List<XmlStudentGrade> Load(string filePath)
    {
        var xml = XDocument.Load(filePath);
        return xml.Descendants("Grade")
            .Select(g => new XmlStudentGrade
            {
                FullName = (string)g.Element("FullName"),
                Subject = (string)g.Element("Subject"),
                Evaluation = (string)g.Element("Evaluation")
            }).ToList();
    }
}

public class GradesApiLoader
{
    
}