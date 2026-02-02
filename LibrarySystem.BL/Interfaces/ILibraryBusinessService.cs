using LibrarySystem.Models.Dto;

namespace LibrarySystem.BL.Interfaces
{
    public interface ILibraryBusinessService
    {
        Book? GetBookDetailsWithAuthorCheck(Guid bookId);
    }
}