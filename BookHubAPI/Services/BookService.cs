using BookingClients.Models;
using System.Collections.Generic;
using System.Linq;  // needed for LINQ

namespace BookingClients.Services
{
    public class BookService
    {
        private int nextId = 1; // auto-increment ID
        private readonly List<Book> _books = new();

        public List<Book> GetAllBooks(string author = null, string title = null, int? year = null)
        {
            var query = _books.AsEnumerable();

            if (!string.IsNullOrEmpty(author))
                query = query.Where(b => b.Author == author);

            if (!string.IsNullOrEmpty(title))
                query = query.Where(b => b.Title == title);

            if (year.HasValue)
                query = query.Where(b => b.Year == year.Value);

            return query.ToList();
        }

        public void AddBook(Book book)
        {
            book.Id = nextId;
            nextId++;

            // No need to reset other properties
            _books.Add(book);
        }
    }
}
