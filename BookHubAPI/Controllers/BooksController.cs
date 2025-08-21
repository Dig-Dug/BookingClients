using BookingClients.Models;
using BookingClients.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        [HttpGet]
        public IEnumerable<Book> Get(
            [FromQuery] string? author = null,
            [FromQuery] string? title = null,
            [FromQuery] int? year = null)
        {
            return _bookService.GetAllBooks(author, title, year);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            _bookService.AddBook(book);
            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }
    }
}
