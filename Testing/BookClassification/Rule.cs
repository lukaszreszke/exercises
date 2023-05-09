namespace BookClassification
{
    public class Rule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public List<Book> Books {get; set;}
    }
}