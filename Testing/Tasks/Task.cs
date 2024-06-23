namespace Tasks;

public class Task
{
    public int Id { get; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskStatus Status { get; private set; }
    public int LastChangedBy { get; private set; }

    public Task(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
        Status = TaskStatus.Open;
    }
    
    public void Start(int lastChangedBy)
    {
        if (Status == TaskStatus.Done)
            throw new InvalidOperationException("Cannot start a task that is already done.");
        if (Status == TaskStatus.InProgress)
            return;

        Status = TaskStatus.InProgress;
        LastChangedBy = lastChangedBy;
    }
    
    public void Finish(int lastChangedBy)
    {
        if (Status == TaskStatus.Done)
            return;
        if (Status == TaskStatus.Open)
            throw new InvalidOperationException("Cannot finish a task that is not started.");
        
        Status = TaskStatus.Done;
        LastChangedBy = lastChangedBy;
    }
    
    public void Reopen(int lastChangedBy)
    {
        if (Status == TaskStatus.Open)
            return;
        if (Status == TaskStatus.InProgress)
            throw new InvalidOperationException("Cannot reopen a task that is in progress.");
        Status = TaskStatus.Open;
        LastChangedBy = lastChangedBy;
    }
}