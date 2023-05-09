namespace BookClassification
{
    public class BookV2
    {
    public BookV2(string name)
        {
            if(name.Length < 5) {
                throw new ArgumentException(nameof(name));
            }           
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set;}
    }
}