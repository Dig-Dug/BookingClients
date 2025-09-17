using System.ComponentModel.DataAnnotations;

namespace BookingClients.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MinLength(2, ErrorMessage = "Title must be at least 2 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        [MinLength(2, ErrorMessage = "Author must be at least 2 characters.")]
        public string Author { get; set; }

        [Range(1, 2100, ErrorMessage = "Year must be a valid number.")]
        public int? Year { get; set; }
    }
}




/*using System;


namespace BookingClients.DTOs

{
    public class BookFilterDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public int? Year { get; set; }
    }
}

*/
