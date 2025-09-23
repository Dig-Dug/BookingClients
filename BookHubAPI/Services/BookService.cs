using BookingClients.Data;
using BookingClients.Models;
using BookingClients.DTOs;

namespace BookingClients.Services
{
    public class BookService
    {
        private readonly BookContext _context;

        public BookService(BookContext context)
        {
            _context = context;
        }

        public IEnumerable<BookDTO> GetAllBooks(BookFilterDTO? filter = null)
        {
            var query = _context.Books.AsQueryable();

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Title))
                    query = query.Where(b => b.Title.Contains(filter.Title));

                if (!string.IsNullOrEmpty(filter.Author))
                    query = query.Where(b => b.Author.Contains(filter.Author));

                if (filter.Year.HasValue)
                    query = query.Where(b => b.Year == filter.Year);
            }

            return query.Select(b => new BookDTO
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Year = b.Year
            }).ToList();
        }

        public Book? GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(b => b.Id == id);
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public bool UpdateBook(int id, Book updatedBook)
        {
            var existingBook = _context.Books.FirstOrDefault(b => b.Id == id);
            if (existingBook == null)
                return false;

            existingBook.Title = updatedBook.Title;
            existingBook.Author = updatedBook.Author;
            existingBook.Year = updatedBook.Year;

            _context.SaveChanges();
            return true;
        }

        public bool DeleteBook(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return false;

            _context.Books.Remove(book);
            _context.SaveChanges();
            return true;
        }
    }
}
