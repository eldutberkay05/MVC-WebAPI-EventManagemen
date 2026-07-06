namespace EventReservationAPI.Models
{
    public class EventDto
    {
        public int EventId { get; set; }

        public string EventName { get; set; }

        public DateTime EventDate { get; set; }

        public decimal TicketPrice { get; set; }

        public int CategoryId { get; set; }

        public int VenueId { get; set; }

        public string? CategoryName { get; set; }

        public string? VenueName { get; set; }
    }
}