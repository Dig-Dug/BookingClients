using BookingClients.Models;
using BookingClients.Services;
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
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book updatedBook)
        {
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
