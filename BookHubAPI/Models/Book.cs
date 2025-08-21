using System.ComponentModel.DataAnnotations;

namespace BookingClients.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Author { get; set; } = string.Empty;

        [Required]
        public string Title { get; set; } = string.Empty;

        public int Year { get; set; }
    }
}
