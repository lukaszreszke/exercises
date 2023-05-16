namespace TheDocument;

public class UnitTest
{
    [Fact]
    public void Publish_WithEmptyAuthor_ThrowsException()
    {
        var document = DocumentMother.CreateBasicDocument().WithAuthor("");

        Assert.Throws<AuthorMustBeProvidedException>(() => document.Publish());
    }

    [Fact]
    public void Publish_WithEmptyTitle_ThrowsException()
    {
        var document = DocumentMother.CreateBasicDocument().WithTitle("");

        Assert.Throws<TitleMustBeProvidedException>(() => document.Publish());
    }

    [Fact]
    public void Publish_WithLessThanThreeChapters_ThrowsException()
    {
        var document = DocumentMother.CreateBasicDocument();
        document.Chapters.RemoveAt(2);

        Assert.Throws<AtLeastThreeChaptersMustBeProvidedException>(() => document.Publish());
    }

    [Fact]
    public void Publish_WithChapterHavingEmptyTitle_ThrowsException()
    {
        var document = DocumentMother.CreateBasicDocument();
        document.Chapters[0].WithTitle("");

        Assert.Throws<ChapterTitleMustBeProvidedException>(() => document.Publish());
    }

    [Fact]
    public void Publish_WithChapterHavingLessThanTenWords_ThrowsException()
    {
        var document = DocumentMother.CreateBasicDocument();
        document.Chapters[0].WithContent("Few words", "Bla");

        Assert.Throws<ChapterContentMustContainAtLeastTenWordsException>(() => document.Publish());
    }

    [Fact]
    public void Publish_WithValidDocument_SetsIsPublishedToTrue()
    {
        var document = DocumentMother.CreateBasicDocument();

        document.Publish();

        Assert.True(document.IsPublished);
    }
}