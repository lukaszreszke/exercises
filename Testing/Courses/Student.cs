namespace Courses;

public class Student
{
    public string FirstName { get; }
    public string LastName { get; }
    public Guid Id { get; }

    public Student(string firstName, string lastName)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
    }

    public void Enroll(ICourse course)
    {
        if (course.HasAvailableSpaceLeft())
            course.Enroll(this);
    }
}