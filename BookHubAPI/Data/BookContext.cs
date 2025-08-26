using Microsoft.EntityFrameworkCore;
using BookingClients.Models;   // FIXED: now points to the real Book model

namespace BookingClients.Data   // FIXED namespace
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
