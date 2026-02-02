using LibrarySystem.BL.Interfaces;
using LibrarySystem.DL.Interfaces;
using LibrarySystem.Models.Dto;
using Moq;
using Xunit;

namespace LibrarySystem.Test
{
    public class LibraryBusinessServiceTests
    {
        Mock<IBookRepository> _bookRepositoryMock;
        Mock<IAuthorRepository> _authorRepositoryMock;

        [Fact]
        public void GetBookDetails_Return_Ok()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            var authorId = Guid.NewGuid();
            var bookId = Guid.NewGuid();

            _bookRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Models.Dto.Book
            {
                Id = bookId,
                Title = "Harry Potter",
                Price = 30,
                AuthorId = authorId
            });

            _authorRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Models.Dto.Author
            {
                Id = authorId,
                Name = "Vlado Delchev"
            });

            var libraryService = new BL.Services.LibraryBusinessService(_bookRepositoryMock.Object, _authorRepositoryMock.Object);

            var result = libraryService.GetBookDetailsWithAuthorCheck(bookId);

            Assert.NotNull(result);
            Assert.Equal(bookId, result.Id);
            Assert.Equal("Harry Potter", result.Title);
        }

        [Fact]
        public void GetBookDetails_When_Author_Missing()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            var authorId = Guid.NewGuid();
            var bookId = Guid.NewGuid();

            _bookRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Models.Dto.Book
            {
                Id = bookId,
                Title = "The Little Prince",
                Price = 22,
                AuthorId = authorId
            });

            _authorRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((Author)null);

            var libraryService = new BL.Services.LibraryBusinessService(_bookRepositoryMock.Object, _authorRepositoryMock.Object);

            var result = libraryService.GetBookDetailsWithAuthorCheck(bookId);

            Assert.Null(result);
        }
    }
}