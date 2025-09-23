namespace BookingClients.DTOs
{
    public class BookFilterDTO
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
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
