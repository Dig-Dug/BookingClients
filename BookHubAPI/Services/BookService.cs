using BookingClients.Data;     // FIXED: points to the corrected namespace
using BookingClients.Models;   // FIXED

namespace BookingClients.Services   // FIXED namespace
{
    public class BookService
    {
        private readonly BookContext _context;

        public BookService(BookContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetBooks()
        {
            return _context.Books.ToList();
        }

        public IEnumerable<Book> GetAllBooks(string? author = null, string? title = null, int? year = null)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(author))
                query = query.Where(b => b.Author.Contains(author));

            if (!string.IsNullOrEmpty(title))
                query = query.Where(b => b.Title.Contains(title));

            if (year.HasValue)
                query = query.Where(b => b.Year == year.Value);

            return query.ToList();
        }


        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

    }
}
