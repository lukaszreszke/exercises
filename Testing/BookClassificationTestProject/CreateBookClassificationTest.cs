using BookClassification;
using Microsoft.EntityFrameworkCore;

namespace BookClassificationTestProject;

[TestClass]
public class CreateBookClassificationTest
{
    private DbContextOptions<BookContext> _options;

    [TestInitialize]
    public void Initialize()
    {
        _options = new DbContextOptionsBuilder<BookContext>()
            .UseInMemoryDatabase(databaseName: "Books")
            .Options;
    }


    [TestMethod]
    public void Something()
    {
         using (var context = new BookContext(_options))
        {
            var todoItem = new Book { Name = "Batman" };
            context.Books.Add(todoItem);
            context.SaveChanges();

            var books = context.Books.ToList();
            Assert.AreEqual(1, books.Count);
            Assert.AreEqual("Batman", books[0].Name);
        }
    }
}