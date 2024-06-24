namespace Acme.TemplateMethod._2;

public abstract class DataProcessor
{
    private readonly DesiredDataContext _context;

    public DataProcessor(DesiredDataContext context)
    {
        _context = context;
    }
    
    protected List<DesiredDataStructure> _result; 
    
    public void Process()
    {
        FetchData();
        ProcessData();
        SaveDataToDatabase();
    }
    
    protected abstract void FetchData();
    protected abstract void ProcessData();

    private void SaveDataToDatabase()
    {
        _context.Data.AddRange(_result);
        _context.SaveChanges();
    }
}