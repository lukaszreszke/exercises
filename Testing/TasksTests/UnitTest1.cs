namespace TasksTests;
using Task = Tasks.Task;
using TaskStatus = Tasks.TaskStatus;

public class TaskTests
{
    [Fact]
    public void CreateTask_SetsInitialValues()
    {
        var task = new Task(1, "Test Task", "Test Description");

        Assert.Equal(1, task.Id);
        Assert.Equal("Test Task", task.Name);
        Assert.Equal("Test Description", task.Description);
        Assert.Equal(TaskStatus.Open, task.Status);
    }

    [Fact]
    public void StartTask_ChangesStatusToInProgress()
    {
        var task = new Task(1, "Test Task", "Test Description");

        task.Start(123);

        Assert.Equal(TaskStatus.InProgress, task.Status);
        Assert.Equal(123, task.LastChangedBy);
    }

    [Fact]
    public void StartTask_ThrowsExceptionWhenTaskIsDone()
    {
        var task = new Task(1, "Test Task", "Test Description");
        task.Start(123);
        task.Finish(123);

        Assert.Throws<InvalidOperationException>(() => task.Start(123));
    }

    [Fact]
    public void FinishTask_ChangesStatusToDone()
    {
        var task = new Task(1, "Test Task", "Test Description");
        task.Start(123);

        task.Finish(123);

        Assert.Equal(TaskStatus.Done, task.Status);
        Assert.Equal(123, task.LastChangedBy);
    }

    [Fact]
    public void FinishTask_ThrowsExceptionWhenTaskIsOpen()
    {
        var task = new Task(1, "Test Task", "Test Description");

        Assert.Throws<InvalidOperationException>(() => task.Finish(123));
    }

    [Fact]
    public void ReopenTask_ChangesStatusToOpen()
    {
        var task = new Task(1, "Test Task", "Test Description");
        task.Start(123);
        task.Finish(123);

        task.Reopen(123);

        Assert.Equal(TaskStatus.Open, task.Status);
        Assert.Equal(123, task.LastChangedBy);
    }

    [Fact]
    public void ReopenTask_ThrowsExceptionWhenTaskIsInProgress()
    {
        var task = new Task(1, "Test Task", "Test Description");
        task.Start(123);

        Assert.Throws<InvalidOperationException>(() => task.Reopen(123));
    }
}