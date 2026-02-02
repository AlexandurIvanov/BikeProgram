using FluentValidation;
using LibrarySystem.BL.Interfaces;
using LibrarySystem.Models.Dto;
using LibrarySystem.Models.Requests;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookCrudService _bookCrudService;
        private readonly IMapper _mapper;
        private readonly IValidator<AddBookRequest> _validator;

        public BooksController(
            IBookCrudService bookCrudService,
            IMapper mapper,
            IValidator<AddBookRequest> validator)
        {
            _bookCrudService = bookCrudService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost("Add")]
        public IActionResult AddBook([FromBody] AddBookRequest request)
        {
            if (request == null) return BadRequest("Book data is null.");

            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var book = _mapper.Map<Book>(request);

            _bookCrudService.AddBook(book);

            return Ok("Book created successfully");
        }

        [HttpGet("GetById")]
        public IActionResult GetById(Guid id)
        {
            var book = _bookCrudService.GetById(id);

            if (book == null)
                return NotFound("Book not found");

            return Ok(book);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var books = _bookCrudService.GetAllBooks();
            return Ok(books);
        }
    }
}