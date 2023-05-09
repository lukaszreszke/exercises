using BookClassification;
using Microsoft.EntityFrameworkCore;

var options = new DbContextOptionsBuilder<BookContext>()
    .UseInMemoryDatabase(databaseName: "books")
    .Options;

using (var context = new BookContext(options))
{
    var category = new Category { Name = "A" };
    context.Categories.Add(category);
    var book = new Book { Name = "Batman" };
    context.Books.Add(book);
    context.SaveChanges();
    var createBookClassification = new CreateBookClassification(context);
    createBookClassification.Classify(1, 1);
}

Console.ReadLine();
