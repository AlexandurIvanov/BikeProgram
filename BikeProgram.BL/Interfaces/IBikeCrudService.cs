using LibrarySystem.Models.Dto;

namespace LibrarySystem.BL.Interfaces
{
    public interface IBookCrudService
    {
        List<Book> GetAllBooks();
        Book? GetById(Guid id);
        void AddBook(Book book);
        void DeleteBook(Guid id);
    }
}
