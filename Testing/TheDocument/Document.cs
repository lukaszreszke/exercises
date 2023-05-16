namespace TheDocument;

public class Document
{
    public string Title { get; set; }
    public string Author { get; set; }
    public List<Chapter> Chapters { get; set; }
    public bool IsPublished { get; private set; }

    public void Publish()
    {
        if (string.IsNullOrEmpty(Author))
            throw new AuthorMustBeProvidedException();
        if (string.IsNullOrEmpty(Title))
            throw new TitleMustBeProvidedException();
        if (Chapters.Count < 3)
            throw new AtLeastThreeChaptersMustBeProvidedException();

        foreach (var chapter in Chapters)
        {
            if (string.IsNullOrEmpty(chapter.Title))
                throw new ChapterTitleMustBeProvidedException();
            if (chapter.Content.Any(x => string.IsNullOrEmpty(x.Content)))
                throw new ChapterContentMustBeProvidedException();
            if (chapter.Content.Any(x => x.Content.Split(' ').Length < 10))
                throw new ChapterContentMustContainAtLeastTenWordsException();
        }

        IsPublished = true;
    }
}

public class ChapterContentMustContainAtLeastTenWordsException : Exception
{
}

public class AtLeastThreeChaptersMustBeProvidedException : Exception
{
}

public class ChapterContentMustBeProvidedException : Exception
{
}

public class ChapterTitleMustBeProvidedException : Exception
{
}

public class AuthorMustBeProvidedException : Exception
{
}

public class TitleMustBeProvidedException : Exception
{
}

public class Chapter
{
    public string Title { get; set; }
    public List<Section> Content { get; set; }
}

public class Section
{
    public string Title { get; set; }
    public string Content { get; set; }
}