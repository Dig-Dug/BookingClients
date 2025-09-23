using BookingClients.Models;
using BookingClients.Services;
using BookingClients.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookingClients.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        // GET /books?title=...&author=...&year=...
        [HttpGet]
        public IEnumerable<BookDTO> Get([FromQuery] BookFilterDTO? filter)
        {
            return _bookService.GetAllBooks(filter);
        }

        // GET /books/{id}
        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            if (id <= 0)
                return BadRequest(new { error = "Invalid book ID." });

            var book = _bookService.GetBookById(id);
            if (book == null)
                return NotFound(new { error = $"Book with ID {id} not found." });

            var bookDto = new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Year = book.Year
            };

            return Ok(bookDto);
        }

        // POST /books
        [HttpPost]
        public IActionResult AddBook([FromBody] BookDTO bookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (bookDto.Year.HasValue && (bookDto.Year < 1 || bookDto.Year > DateTime.Now.Year))
                return BadRequest(new { error = "Invalid year. Must be between 1 and the current year." });

            var book = new Book
            {
                Title = bookDto.Title.Trim(),
                Author = bookDto.Author.Trim(),
                Year = bookDto.Year ?? 0
            };

            try
            {
                _bookService.AddBook(book);

                return CreatedAtAction(
                    nameof(GetBook),
                    new { id = book.Id },
                    new BookDTO
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Author = book.Author,
                        Year = book.Year
                    });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "An error occurred while saving the book." });
            }
        }

        // PUT /books/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] BookDTO bookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(bookDto.Title) || string.IsNullOrWhiteSpace(bookDto.Author))
                return BadRequest(new { error = "Title and Author cannot be empty." });

            if (bookDto.Year.HasValue && (bookDto.Year < 1 || bookDto.Year > DateTime.Now.Year))
                return BadRequest(new { error = "Invalid year. Must be between 1 and the current year." });

            var book = _bookService.GetBookById(id);
            if (book == null)
                return NotFound(new { error = $"Book with ID {id} not found." });

            book.Title = bookDto.Title.Trim();
            book.Author = bookDto.Author.Trim();
            book.Year = bookDto.Year ?? book.Year;

            var success = _bookService.UpdateBook(id, book);
            if (!success)
                return StatusCode(500, new { error = "Failed to update the book." });

            return Ok(new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Year = book.Year
            });
        }

        // DELETE /books/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var success = _bookService.DeleteBook(id);
            if (!success)
                return NotFound(new { error = $"Book with ID {id} not found." });

            return NoContent(); // 204
        }
    }
}
