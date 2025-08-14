using BookingClients.Models;
using System.Collections.Generic;

namespace BookingClients.Services
{
    public class BookService
    {
        private readonly List<Book> _books = new()
        {
            new Book { Id = 1, Author = "J.K. Rowling", Title = "Harry Potter", Year = 1997 },
            new Book { Id = 2, Author = "George Orwell", Title = "1984", Year = 1949 },
            new Book { Id = 3, Author = "J.R.R. Tolkien", Title = "The Hobbit", Year = 1937 }
        };

        public List<Book> GetAllBooks() => _books;

        public void AddBook(Book book) => _books.Add(book);
    }
}
