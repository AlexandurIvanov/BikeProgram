namespace LibrarySystem.Models.Requests
{
    public class AddBookRequest
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public Guid AuthorId { get; set; }
    }
}