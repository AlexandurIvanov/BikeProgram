using LibrarySystem.BL.Interfaces;
using LibrarySystem.DL.Interfaces;
using LibrarySystem.Models.Dto;

namespace LibrarySystem.BL.Services
{
    public class BookCrudService : IBookCrudService
    {
        private readonly IBookRepository _bookRepository;

        public BookCrudService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public void AddBook(Book book)
        {
            if (book == null) return;

            if (book.Id == Guid.Empty)
            {
                book.Id = Guid.NewGuid();
            }

            _bookRepository.AddBook(book);
        }

        public void DeleteBook(Guid id)
        {
            _bookRepository.DeleteBook(id);
        }

        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        public Book? GetById(Guid id)
        {
            return _bookRepository.GetById(id);
        }
    }
}