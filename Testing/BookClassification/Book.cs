namespace BookClassification
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int RuleId {get; set;}
        public Rule Rule { get; set; }
        public string? CategoryAssignedType { get; set; }
    }
}