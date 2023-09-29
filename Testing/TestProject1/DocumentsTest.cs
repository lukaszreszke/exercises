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
        var document = new Document();

        document.Verify();

        Assert.Equal("VERIFIED", document.Status.Code);
    }

    [Fact]
    public void publish_verified_document()
    {
        var document = new Document();
        document.Verify();

        document.Publish();

        Assert.Equal("PUBLISHED", document.Status.Code);
    }

    [Fact]
    public void cannot_publish_non_verified_document()
    {
        var document = new Document();

        Assert.Throws<CannotPublishUnverifiedDocument>(() => document.Publish());

        Assert.Equal("DRAFT", document.Status.Code);
    }

    [Fact]
    public void cannot_verify_non_draft_document()
    {
        var document = new Document();
        
        document.Verify();
        document.Status = new Status { Code = AvailableStatuses.PUBLISHED.ToString() };
        
        Assert.Throws<CannotVerifyPublishedDocument>(() => document.Verify());
        Assert.Equal("PUBLISHED", document.Status.Code); 
    }
}