namespace BookClassification
{
    public class CreateBookClassification
    {
        private readonly BookContext _context;
        public CreateBookClassification(BookContext context)
        {
            _context = context;
        }
        public void Classify(int animalId, int categoryId, Rule rule = null)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == animalId);
            var category = _context.Categories.FirstOrDefault(x => x.Id == categoryId);

            if (book == null)
            {
                throw new InvalidDataException();
            }
            else
            {
                if (category != null && book.Category != category)
                {
                    if (rule != null)
                    {
                        book.CategoryAssignedType = "rule";
                        book.Category = category;
                        book.Rule = rule;
                    }
                    else
                    {
                        book.CategoryAssignedType = "manual";
                        book.Category = category;
                    }
                }
                else if (category != null && (book.CategoryAssignedType == null || book.CategoryAssignedType == "auto"))
                {
                    book.CategoryAssignedType = rule == null ? "manual" : "rule";
                }

                _context.SaveChanges();

            }
        }
    }
}