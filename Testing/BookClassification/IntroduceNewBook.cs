namespace BookClassification
{
    public class IntroduceNewBook
    {
        private readonly BookContext _ctx;
        private decimal _initialPrice;

        public IntroduceNewBook(string name, int initialQuantity, decimal basePrice, string? category = null)
        {
            _ctx = new BookContext(BookContext.DefaultOptions());
            _initialPrice = decimal.Zero;
            if (!string.IsNullOrEmpty(category))
            {
                var bookPrice = _ctx.BookPrices.FirstOrDefault(x => x.Category == category);
                if (bookPrice != null)
                {
                    _initialPrice = bookPrice.Price;
                }
            }
        }

        public void Add(string name)
        {
            _ctx.BooksV3.Add(new BookV3 { Name = name, Price = _initialPrice });
            _ctx.SaveChanges();
        }
    }
}