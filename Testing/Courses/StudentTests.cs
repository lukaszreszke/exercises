using FluentAssertions;
using Xunit;

namespace Courses
{
    public class StudentTests
    {
        [Fact]
        public void Enrollment_succeeds_if_there_are_places_left()
        {
            // Arrange
            var student = new Student("John", "Doe");
            var course = new Course("Testing in C#");
            course.SetStudentsLimit(10);

            // Act
            student.Enroll(course);

            // Assert
            course.IsEnrolled(student).Should().BeTrue();
        }

        [Fact]
        public void Enrollment_fails_if_there_are_no_places_left()
        {
            // Arrange
            var student1 = new Student("John", "Doe");
            var student2 = new Student("Jane", "Doe");
            var course = new Course("Individual consultations");
            course.SetStudentsLimit(1);
            student1.Enroll(course);

            // Act
            student2.Enroll(course);

            // Assert
            Assert.False(course.IsEnrolled(student2));
        }
    }
}