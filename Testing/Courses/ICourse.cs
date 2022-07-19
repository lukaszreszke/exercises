namespace Courses;

public interface ICourse // Needed because of test
{
    void Enroll(Student student);
    bool IsEnrolled(Student student);
    void SetStudentsLimit(int limit);
    bool HasAvailableSpaceLeft();
}

public class Course : ICourse
{
    public void Enroll(Student student)
    {
        Students.Add(student);
    }
    public bool IsEnrolled(Student student) => Students.Contains(student);
    public void SetStudentsLimit(int limit)
    {
        StudentsLimit = limit;
    }
    public bool HasAvailableSpaceLeft()
    {
        return CurrentNumberOfStudents + 1 < StudentsLimit;
    }

    public Course(string title)
    {
        Title = title;
        Students = new List<Student>();
    }

    private string Title { get; }
    private int StudentsLimit { get; set; }
    private int CurrentNumberOfStudents { get; }
    private List<Student> Students { get; }
}