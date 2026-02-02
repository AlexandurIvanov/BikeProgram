using LibrarySystem.Models.Dto;

namespace LibrarySystem.DL.Interfaces
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks();
        Book? GetById(Guid? id);
        void AddBook(Book book);
        void DeleteBook(Guid? id);
    }
}