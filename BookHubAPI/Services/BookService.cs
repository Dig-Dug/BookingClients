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

        public IEnumerable<Book> GetBooks()
        {
            return _context.Books.ToList();
        }

        /*   public IEnumerable<Book> GetAllBooks(string? author = null, string? title = null, int? year = null)
           {
               var query = _context.Books.AsQueryable();

               if (!string.IsNullOrEmpty(author))
                   query = query.Where(b => b.Author.Contains(author));

               if (!string.IsNullOrEmpty(title))
                   query = query.Where(b => b.Title.Contains(title));

               if (year.HasValue)
                   query = query.Where(b => b.Year == year.Value);

               return query.ToList();
           }*/
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




        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

    }
}
