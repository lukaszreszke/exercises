using Microsoft.EntityFrameworkCore;

namespace BookClassification
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookV2> BooksV2 {get; set;}
        public DbSet<Rule> Rules { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(p => p.Category)
                .WithMany(a => a.Books)
                .HasForeignKey(x => x.CategoryId);

            modelBuilder.Entity<Book>()
            .HasOne(a => a.Rule)
            .WithMany(x => x.Books)
            .HasForeignKey(x => x.RuleId);
        }

        public static DbContextOptions<BookContext> DefaultOptions()
        {
            return new DbContextOptionsBuilder<BookContext>()
                .UseInMemoryDatabase(databaseName: "books")
                .Options;
        }
    }
}