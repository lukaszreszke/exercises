namespace TheDocument;

public static class DocumentExtensions
{
    public static Document WithAuthor(this Document document, string author)
    {
        document.Author = author;
        return document;
    }

    public static Document WithTitle(this Document document, string title)
    {
        document.Title = title;
        return document;
    }

    public static Document AddChapter(this Document document, Chapter chapter)
    {
        document.Chapters.Add(chapter);
        return document;
    }
}

public static class ChapterExtensions
{
    public static Chapter WithTitle(this Chapter chapter, string title)
    {
        chapter.Title = title;
        return chapter;
    }

    public static Chapter WithContent(this Chapter chapter, string content, string title)
    {
        if (chapter.Content == null) chapter.Content = new List<Section>();
        chapter.Content.Add(new Section { Content = content, Title = title});
        return chapter;
    }
}

public static class DocumentMother
{
    public static Document CreateBasicDocument()
    {
        var document = new Document
            {
                Chapters = new List<Chapter>()
            }.WithTitle("Example Document")
            .WithAuthor("Jan Kowalski");

        for (int i = 0; i < 3; i++)
        {
            var chapter = new Chapter().WithTitle($"Chapter {i + 1}")
                .WithContent("This is a content of a chapter with more than ten words.", "Test");
            document.AddChapter(chapter);
        }

        return document;
    }
}
