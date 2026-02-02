namespace LibrarySystem.Models.Dto
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public Guid AuthorId { get; set; }
    }
}