namespace TheDocument;

public class UnitTest
{
    [Fact]
    public void Publish_WithLessThanThreeChapters_ThrowsException()
    {
        var document = new Document
        {
            Author = "Author",
            Title = "Title",
            Chapters = new List<Chapter>
            {
                new Chapter { Title = "Chapter 1", Content = "Some content here" },
                new Chapter { Title = "Chapter 2", Content = "Some other content here" }
            }
        };

        Assert.Throws<AtLeastThreeChaptersMustBeProvidedException>(() => document.Publish());
    }

    [Fact]
    public void Publish_WithChapterHavingLessThanTenWords_ThrowsException()
    {
        var document = new Document
        {
            Author = "Author",
            Title = "Title",
            Chapters = new List<Chapter>
            {
                new Chapter { Title = "Chapter 1", Content = "Few words" },
                new Chapter { Title = "Chapter 2", Content = "Some other content here" },
                new Chapter { Title = "Chapter 3", Content = "Additional content here" }
            }
        };

        Assert.Throws<ChapterContentMustContainAtLeastTenWordsException>(() => document.Publish());
    }

    [Fact]
    public void Publish_WithValidDocument_SetsIsPublishedToTrue()
    {
        var document = new Document
        {
            Author = "Author",
            Title = "Title",
            Chapters = new List<Chapter>
            {
                new Chapter { Title = "Chapter 1", Content = "Some great and smart content here with more than ten words" },
                new Chapter { Title = "Chapter 2", Content = "Some other great and smart content here with more than ten words" },
                new Chapter { Title = "Chapter 3", Content = "Additional smart and great content here with more than ten words" }
            }
        };

        document.Publish();

        Assert.True(document.IsPublished);
    }

}