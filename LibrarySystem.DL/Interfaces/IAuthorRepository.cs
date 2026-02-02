using LibrarySystem.Models.Dto;

namespace LibrarySystem.DL.Interfaces
{
    public interface IAuthorRepository
    {
        List<Author> GetAllAuthors();
        Author? GetById(Guid id);
        void AddAuthor(Author author);
        void DeleteAuthor(Guid id);
    }
}