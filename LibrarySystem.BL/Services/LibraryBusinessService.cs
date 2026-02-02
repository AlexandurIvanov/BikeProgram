using LibrarySystem.BL.Interfaces;
using LibrarySystem.DL.Interfaces;
using LibrarySystem.Models.Dto;

namespace LibrarySystem.BL.Services
{
    public class LibraryBusinessService : ILibraryBusinessService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public LibraryBusinessService(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public Book? GetBookDetailsWithAuthorCheck(Guid bookId)
        {
            var book = _bookRepository.GetById(bookId);
            if (book == null) return null;

            var author = _authorRepository.GetById(book.AuthorId);

            if (author == null)
            {
                return null;
            }

            return book;
        }
    }
}