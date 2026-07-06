namespace EventReservationMVC.Models
{
    public class Event
    {
        public int EventId { get; set; }

        public string EventName { get; set; }

        public DateTime EventDate { get; set; }

        public decimal TicketPrice { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public int VenueId { get; set; }
        public Venue? Venue { get; set; }
    }
}