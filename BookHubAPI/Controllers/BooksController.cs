using BookingClients.Models;
using BookingClients.Services;
using BookingClients.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
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

        /*  [HttpGet]
          public IEnumerable<Book> Get(
              [FromQuery] string? author = null,
              [FromQuery] string? title = null,
              [FromQuery] int? year = null)
          {
              return _bookService.GetAllBooks(author, title, year);
          }*/

        [HttpGet]
        public IEnumerable<BookDTO> Get([FromQuery] BookFilterDTO? filter)
        {
            return _bookService.GetAllBooks(filter);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] BookDTO bookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (bookDto.Year.HasValue && (bookDto.Year < 1 || bookDto.Year > DateTime.Now.Year))
                return BadRequest("Invalid year. Must be between 1 and the current year.");

            var book = new Book
            {
                Title = bookDto.Title.Trim(),
                Author = bookDto.Author.Trim(),
                Year = bookDto.Year ?? 0
            };

            _bookService.AddBook(book);

            return CreatedAtAction(nameof(Get), new { id = book.Id }, bookDto);
        }



        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book updatedBook)
        {
            if (string.IsNullOrWhiteSpace(updatedBook.Title) || string.IsNullOrWhiteSpace(updatedBook.Author))
                return BadRequest("Title and Author cannot be empty.");

            using var connection = new SqliteConnection("Data Source=books.db");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
        UPDATE Books
        SET Title = $title, Author = $author
        WHERE Id = $id;
    ";
            command.Parameters.AddWithValue("$id", id);
            command.Parameters.AddWithValue("$title", updatedBook.Title);
            command.Parameters.AddWithValue("$author", updatedBook.Author);

            var rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0 ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            using var connection = new SqliteConnection("Data Source=books.db");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Books WHERE Id = $id;";
            command.Parameters.AddWithValue("$id", id);

            var rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0 ? Ok() : NotFound();
        }
    }
}
