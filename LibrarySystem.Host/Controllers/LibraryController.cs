using LibrarySystem.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryBusinessService _libraryBusinessService;

        public LibraryController(ILibraryBusinessService libraryBusinessService)
        {
            _libraryBusinessService = libraryBusinessService;
        }

        [HttpGet("GetBookDetails")]
        public IActionResult GetBookWithAuthorCheck(Guid bookId)
        {
            var result = _libraryBusinessService.GetBookDetailsWithAuthorCheck(bookId);

            if (result == null)
            {
                return NotFound("Book or Author not found.");
            }

            return Ok(result);
        }
    }
}