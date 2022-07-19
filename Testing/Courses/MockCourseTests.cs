using Moq;
using Xunit;

namespace Courses;

public class MockCourseTests
{
    [Fact]
    public void Enrollment_succeeds_if_there_are_places_left()
    {
        // Arrange
        var student = new Student("John", "Doe");
        var courseMock = new Mock<ICourse>();
        courseMock.Setup(x => x.HasAvailableSpaceLeft()).Returns(true);
        var course = courseMock.Object;

        // Act
        student.Enroll(course);

        // Assert
        courseMock.Verify(x => x.Enroll(student), Times.Once);
        courseMock.Verify(x => x.HasAvailableSpaceLeft(), Times.Once);
        courseMock.VerifyNoOtherCalls();
    }

    [Fact]
    public void Enrollment_fails_if_there_are_no_places_left()
    {
        // Arrange
        var student1 = new Student("John", "Doe");
        var student2 = new Student("Jane", "Doe");
        var courseMock = new Mock<ICourse>();
        courseMock.Setup(x => x.HasAvailableSpaceLeft()).Returns(false);
        var course = courseMock.Object;
        student1.Enroll(course);

        // Act
        student2.Enroll(course);

        // Assert
        Assert.False(course.IsEnrolled(student2));
        courseMock.Verify(x => x.Enroll(student2), Times.Never);
    }
}