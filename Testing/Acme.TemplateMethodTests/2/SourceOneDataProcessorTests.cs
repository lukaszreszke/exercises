using Acme.TemplateMethod._2;
using Microsoft.EntityFrameworkCore;

namespace Acme.TemplateMethodTests._2;

public class SourceOneDataProcessorTests
{
    [Fact]
    public void ProcessData_ConvertsSourceOneDataToDesiredDataStructure()
    {
        var context = DesiredDataContext();
        var processor = new SourceOneDataProcessor(new SourceOneDataProvider(), context);

        processor.Process();

        var result = context.Data.ToList();
        Assert.NotEmpty(result);
    }
    
    private DesiredDataContext DesiredDataContext()
    {
        var options = new DbContextOptionsBuilder<DesiredDataContext>()
            .UseInMemoryDatabase(databaseName: "testdb")
            .Options;
        
        var context = new DesiredDataContext(options);
        context.Database.EnsureCreated();
        
        return context;
    }
}