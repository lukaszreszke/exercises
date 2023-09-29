using Tests.Documents;

namespace TestProject1;

public class DocumentsTest
{
    [Fact]
    public void draft_is_default_status()
    {
        var document = new Document();
        Assert.Equal("DRAFT", document.Status.Code);
    }

    [Fact]
    public void verify_draft_document()
    {
        
    }

    [Fact]
    public void publish_verified_document()
    {
        
    }

    [Fact]
    public void cannot_publish_non_verified_document()
    {
        
    }

    [Fact]
    public void cannot_verify_non_draft_document()
    {
        
    }
}