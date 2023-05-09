using BookClassification;

namespace BookClassificationTestProject;

[TestClass]
public class BookV2Test
{
    [TestMethod]
    public void BookV2Bug(){
        using var context = new BookContext(BookContext.DefaultOptions());

        context.BooksV2.Add(new BookV2("testing"));
        context.SaveChanges();
    }
}
